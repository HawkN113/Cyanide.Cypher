using System.Text;
using Antlr4.Runtime;
using Cyanide.Cypher.Translators.Abstraction;

namespace Cyanide.Cypher.Translators;

internal class SqlToCypherTranslator: SQLBaseVisitor<string>, IQueryTranslator
{
    private readonly StringBuilder _cypherQuery = new();

    public string Translate(string inputQuery)
    {
        var lexer = new SQLLexer(new AntlrInputStream(inputQuery));
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new SQLParser(tokenStream);
        var tree = parser.sqlStatement();
        
        Visit(tree);
        
        return _cypherQuery.ToString();
    }

    public override string VisitSelectStatement(SQLParser.SelectStatementContext context)
    {
        var selectElements = context.selectElements().GetText();
        var tableSource = context.tableSource().GetText();
        _cypherQuery.AppendLine($"MATCH (n:{tableSource})");

        if (context.whereClause() != null)
        {
            Visit(context.whereClause());
        }

        if (context.groupByClause() != null)
        {
            Visit(context.groupByClause());
        }

        if (context.havingClause() != null)
        {
            Visit(context.havingClause());
        }

        _cypherQuery.AppendLine($"RETURN {selectElements.Replace(",", ", n.")};");
        return _cypherQuery.ToString();
    }

    public override string VisitWhereClause(SQLParser.WhereClauseContext context)
    {
        var condition = context.condition().GetText();
        _cypherQuery.AppendLine($"WHERE {condition.Replace("=", ":")}");
        return null!;
    }

    public override string VisitGroupByClause(SQLParser.GroupByClauseContext context)
    {
        var groupByColumns = context.columnName().Select(col => col.GetText());
        _cypherQuery.AppendLine($"WITH {string.Join(", ", groupByColumns)}");
        return null!;
    }

    public override string VisitHavingClause(SQLParser.HavingClauseContext context)
    {
        var condition = context.condition().GetText();
        _cypherQuery.AppendLine($"WHERE {condition}");
        return null!;
    }
}