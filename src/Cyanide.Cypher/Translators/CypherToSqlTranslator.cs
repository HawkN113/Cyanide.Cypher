using System.Text;
using Antlr4.Runtime;
using Cyanide.Cypher.Translators.Abstraction;

namespace Cyanide.Cypher.Translators;

internal class CypherToSqlTranslator: CypherBaseVisitor<string>, IQueryTranslator
{
    private readonly StringBuilder _joinQuery = new();
    private readonly StringBuilder _selectQuery = new();
    private readonly StringBuilder _whereQuery = new();
    private readonly SyntaxErrorListener _syntaxErrorListener = new();

    public string Translate(string inputQuery)
    {
        var lexer = new CypherLexer(new AntlrInputStream(inputQuery));
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new CypherParser(tokenStream);
        
        parser.AddErrorListener(_syntaxErrorListener);
        var tree = parser.cypherQuery();

        var errors = _syntaxErrorListener.GetErrorCount();
        if(errors > 0)
            throw new InvalidOperationException($"Found {errors} errors");
        
        Visit(tree);
        
        return $"{_selectQuery} {_joinQuery}{_whereQuery}";
    }

    public override string VisitCypherQuery(CypherParser.CypherQueryContext context)
    {
        Visit(context.matchClause());
        if (context.returnClause() != null)
        {
            Visit(context.returnClause());
        }
        if (context.whereClause() != null)
        {
            Visit(context.whereClause());
        }
        return null!;
    }

    public override string VisitMatchClause(CypherParser.MatchClauseContext context)
    {
        _joinQuery.Append("FROM ");
        Visit(context.matchPattern(0));
        for (var i = 1; i < context.matchPattern().Length; i++)
        {
            _joinQuery.Append(", ");
            Visit(context.matchPattern(i));
        }
        return null!;
    }

    public override string VisitMatchPattern(CypherParser.MatchPatternContext context)
    {
        var relationshipLabelNode = context.relationshipLabel();
        var relationshipAliasNode = context.relationshipAlias();

        if (context.alias() == null || context.label() == null) return null!;
        
        var alias = context.alias(0).GetText();
        var label = context.label(0).GetText();

        if (relationshipLabelNode != null)
        {
            var relationshipAlias = relationshipAliasNode != null ? relationshipAliasNode.GetText() : string.Empty;
            var relationshipLabel = relationshipLabelNode != null ? relationshipLabelNode.GetText() : string.Empty;
            var targetAliasNode = context.alias(1);
            var targetLabelNode = context.label(1);

            var targetAlias = targetAliasNode != null ? targetAliasNode.GetText() : string.Empty;
            var targetLabel = targetLabelNode != null ? targetLabelNode.GetText() : string.Empty;

            //_joinQuery.Append($"{label} AS {alias} INNER {targetLabel} AS {targetAlias} ON {alias}.{relationshipLabel}_id = {targetAlias}.id");
            _joinQuery.Append($"{label} AS {alias} INNER JOIN {targetLabel} AS {targetAlias} ON {alias}.id = {targetAlias}.id");
        }
        else
        {
            _joinQuery.Append($"{label} AS {alias}");
        }
        return null!;
    }

    public override string VisitWhereClause(CypherParser.WhereClauseContext context)
    {
        _whereQuery.Append(" WHERE ");
        Visit(context.condition());
        return null!;
    }

    public override string VisitCondition(CypherParser.ConditionContext context)
    {
        Visit(context.comparison(0));
        for (var i = 1; i < context.comparison().Length; i++)
        {
            var operatorNode = context.logicalOperator(i - 1).GetText();
            _whereQuery.Append($" {operatorNode} ");
            Visit(context.comparison(i));
        }
        return null!;
    }

    public override string VisitComparison(CypherParser.ComparisonContext context)
    {
        var property = context.property().GetText();
        var operatorNode = context.comparisonOperator().GetText();
        var value = context.value().GetText();
        _whereQuery.Append($"{property}{operatorNode}{value}");
        return null!;
    }

    public override string VisitReturnClause(CypherParser.ReturnClauseContext context)
    {
        _selectQuery.Append("SELECT ");
        Visit(context.returnItems());
        return null!;
    }

    public override string VisitReturnItems(CypherParser.ReturnItemsContext context)
    {
        var returnItem = context.returnItem(0).GetText();
        _selectQuery.Append(returnItem);
        for (var i = 1; i < context.returnItem().Length; i++)
        {
            _selectQuery.Append(", ");
            _selectQuery.Append(context.returnItem(i).GetText());
        }
        return null!;
    }
}