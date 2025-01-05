using Antlr4.Runtime;
using Cyanide.Cypher.Translators.Abstraction;

namespace Cyanide.Cypher.Translators;

internal class SqlToCypherTranslator: SQLBaseVisitor<string>, IQueryTranslator
{
    private string _cypherQuery = string.Empty;
    public string Translate(string inputQuery)
    {
        var inputStream = new AntlrInputStream(inputQuery);
        var lexer = new SQLLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new SQLParser(tokenStream);
        var tree = parser.sqlStatement();
        Visit(tree);
        return _cypherQuery;
    }
    
    public override string VisitSelectStatement(SQLParser.SelectStatementContext context)
    {
        var selectElements = context.selectElements().GetText();
        var tableSource = context.tableSource().GetText();
        _cypherQuery += $"MATCH (n:{tableSource})\n";

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

        _cypherQuery += $"RETURN {selectElements.Replace(",", ", n.")};";
        return _cypherQuery;
    }

    public override string VisitWhereClause(SQLParser.WhereClauseContext context)
    {
        var condition = context.condition().GetText();
        _cypherQuery += $"WHERE {condition.Replace("=", ":")}\n";
        return null!;
    }

    public override string VisitGroupByClause(SQLParser.GroupByClauseContext context)
    {
        var groupByColumns = context.columnName().Select(col => col.GetText());
        _cypherQuery += $"WITH {string.Join(", ", groupByColumns)}\n";
        return null!;
    }

    public override string VisitHavingClause(SQLParser.HavingClauseContext context)
    {
        var condition = context.condition().GetText();
        _cypherQuery += $"WHERE {condition}\n";
        return null!;
    }
}