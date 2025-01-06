using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public sealed class CypherQueryBuilder: ICypherQueryBuilder
{
    private readonly StringBuilder _matchClauses = new();
    private readonly StringBuilder _whereClauses = new();
    private readonly StringBuilder _returnClauses = new();

    /// <summary>
    /// Add MATCH clause
    /// </summary>
    /// <param name="configureMatch"></param>
    /// <returns></returns>
    public CypherQueryBuilder Match(Action<MatchBuilder> configureMatch)
    {
        var matchBuilder = new MatchBuilder(this, _matchClauses);
        configureMatch(matchBuilder);
        return this;
    }

    /// <summary>
    /// Add RETURN clause
    /// </summary>
    /// <param name="configureReturn"></param>
    /// <returns></returns>
    public CypherQueryBuilder Return(Action<ReturnBuilder> configureReturn)
    {
        var returnBuilder = new ReturnBuilder(this, _returnClauses);
        configureReturn(returnBuilder);
        return this;
    }
    
    /// <summary>
    /// Add WHERE clause
    /// </summary>
    /// <param name="configureWhere"></param>
    /// <returns></returns>
    public CypherQueryBuilder Where(Action<WhereBuilder> configureWhere)
    {
        var whereBuilder = new WhereBuilder(this, _whereClauses);
        configureWhere(whereBuilder);
        return this;
    }

    /// <summary>
    /// Build the Cypher query
    /// </summary>
    /// <returns></returns>
    public string Build()
    {
        StringBuilder queryBuilder = new();
        AppendClause(_matchClauses, queryBuilder);
        AppendClause(_whereClauses, queryBuilder);
        AppendClause(_returnClauses, queryBuilder);
        return queryBuilder.ToString().Trim();
    }

    private static void AppendClause(StringBuilder clause, StringBuilder queryBuilder)
    {
        if (clause.Length <= 0) return;
        if (queryBuilder.Length > 0)
        {
            queryBuilder.Append(' ');
        }
        queryBuilder.Append(clause);
    }
}