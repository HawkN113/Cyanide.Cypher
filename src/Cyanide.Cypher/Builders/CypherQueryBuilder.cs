using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public sealed class CypherQueryBuilder: ICypherQueryBuilder
{
    private readonly StringBuilder _matchClauses = new();
    private readonly StringBuilder _optMatchClauses = new();
    private readonly StringBuilder _whereClauses = new();
    private readonly StringBuilder _returnClauses = new();

    /// <summary>
    /// Add MATCH clause
    /// </summary>
    /// <param name="configureMatch"></param>
    /// <returns></returns>
    public CypherQueryBuilder Match(Func<MatchBuilder, MatchBuilder> configureMatch)
    {
        var matchBuilder = new MatchBuilder(this, _matchClauses);
        configureMatch(matchBuilder).End();
        return this;
    }
    
    /// <summary>
    /// Add OPTIONAL MATCH clause
    /// </summary>
    /// <param name="configureOptMatch"></param>
    /// <returns></returns>
    public CypherQueryBuilder OptionalMatch(Func<OptMatchBuilder, OptMatchBuilder> configureOptMatch)
    {
        var optMatchBuilder = new OptMatchBuilder(this, _optMatchClauses);
        configureOptMatch(optMatchBuilder).End();
        return this;
    }

    /// <summary>
    /// Add RETURN clause
    /// </summary>
    /// <param name="configureReturn"></param>
    /// <returns></returns>
    public CypherQueryBuilder Select(Func<SelectBuilder, SelectBuilder> configureReturn)
    {
        var returnBuilder = new SelectBuilder(this, _returnClauses);
        configureReturn(returnBuilder).End();
        return this;
    }
    
    /// <summary>
    /// Add WHERE clause
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public CypherQueryBuilder Where(Func<WhereBuilder, WhereBuilder> conditions)
    {
        var whereBuilder = new WhereBuilder(this, _whereClauses);
        conditions(whereBuilder).End();
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
        AppendClause(_optMatchClauses, queryBuilder);
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