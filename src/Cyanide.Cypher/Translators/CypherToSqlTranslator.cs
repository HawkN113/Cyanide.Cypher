using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Cyanide.Cypher.Translators.Abstraction;

namespace Cyanide.Cypher.Translators;

internal class CypherToSqlTranslator: CypherBaseVisitor<string>, IQueryTranslator
{
    private string _sqlQuery = string.Empty;
    private string _fromClause = string.Empty;
    private string _whereClause = string.Empty;

    public string Translate(string cypherQuery)
    {
        var lexer = new CypherLexer(new AntlrInputStream(cypherQuery));
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new CypherParser(tokenStream);

        // Parse the Cypher query
        var tree = parser.cypherQuery();

        // Visit the parse tree to generate the SQL query
        Visit(tree);

        // Assemble the final SQL query
        _sqlQuery = $"{_sqlQuery} {_fromClause} {_whereClause}".Trim();
        return _sqlQuery;
    }

    public override string VisitMatchClause(CypherParser.MatchClauseContext context)
    {
        var patterns = context.matchPattern()
            .Select(Visit)
            .Where(match => !string.IsNullOrEmpty(match))
            .ToList();

        _fromClause = $"FROM {string.Join(" JOIN ", patterns)}";
        return _fromClause;
    }

    public override string VisitMatchPattern(CypherParser.MatchPatternContext context)
    {
        var aliasNode = context.alias();
        var labelNode = context.label();

        // Check if alias and label are present and extract text correctly
        if (aliasNode != null && labelNode != null)
        {
            var alias = context.alias(0).GetText();
            var label = context.label(0).GetText();

            // If the pattern involves a relationship, we handle it here
            if (context.relationshipLabel() != null)
            {
                var relationshipAliasNode = context.relationshipAlias();
                var relationshipLabelNode = context.relationshipLabel();
                var targetAliasNode = context.alias(1);
                var targetLabelNode = context.label(1);

                var relationshipAlias = relationshipAliasNode != null ? relationshipAliasNode.GetText() : string.Empty;
                var relationshipLabel = relationshipLabelNode != null ? relationshipLabelNode.GetText() : string.Empty;
                var targetAlias = targetAliasNode != null ? targetAliasNode.GetText() : string.Empty;
                var targetLabel = targetLabelNode != null ? targetLabelNode.GetText() : string.Empty;

                // Construct SQL JOIN query for relationships
                return $"{label} AS {alias} JOIN {targetLabel} AS {targetAlias} ON {alias}.{relationshipLabel}_id = {targetAlias}.id";
            }

            // If it's just a label pattern with an alias, return it as part of the FROM clause
            return $"{label} AS {alias}";
        }

        return string.Empty;
    }

    public override string VisitWhereClause(CypherParser.WhereClauseContext context)
    {
        var condition = Visit(context.condition());
        _whereClause = $"WHERE {condition}";
        return _whereClause;
    }

    public override string VisitCondition(CypherParser.ConditionContext context)
    {
        var conditions = context.comparison()
            .Select(Visit)
            .ToList();

        var operators = context.logicalOperator()
            .Select(op => op.GetText().ToUpper())
            .ToList();

        // Combine conditions with logical operators
        return string.Join(" ", conditions.Zip(operators, (cond, op) => $"{cond} {op}"));
    }

    public override string VisitComparison(CypherParser.ComparisonContext context)
    {
        var property = context.property().GetText();
        var op = context.comparisonOperator().GetText();
        var value = context.value().GetText();

        return $"{property} {op} {value}";
    }

    public override string VisitReturnClause(CypherParser.ReturnClauseContext context)
    {
        var returnItems = context.returnItems()
            .returnItem()
            .Select(Visit)
            .ToList();

        _sqlQuery = $"SELECT {string.Join(", ", returnItems)}";
        return _sqlQuery;
    }

    public override string VisitReturnItem(CypherParser.ReturnItemContext context)
    {
        return context.GetText();
    }
}