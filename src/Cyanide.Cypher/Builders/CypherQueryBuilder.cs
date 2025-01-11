using System.Text;
using Cyanide.Cypher.Builders.Abstraction.Clauses;
using Cyanide.Cypher.Builders.Abstraction.Clauses.Subclauses;
using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders;

internal sealed class CypherQueryBuilder : IQuery, IMatchQuery
{
    private readonly StringBuilder _createClauses = new();
    private readonly StringBuilder _deleteClauses = new();
    private readonly StringBuilder _matchClauses = new();
    private readonly StringBuilder _optMatchClauses = new();
    private readonly StringBuilder _whereClauses = new();
    private readonly StringBuilder _returnClauses = new();
    private readonly StringBuilder _orderByClauses = new();

    /// <summary>
    /// ORDER BY ORDER BY is a sub-clause following RETURN <br/>
    /// Sample: ORDER BY n.name
    /// </summary>
    /// <param name="configureOrderBy"></param>
    /// <returns></returns>
    public IOrderBySubQuery OrderBy(Action<OrderBySubClause> configureOrderBy)
    {
        var orderByBuilder = new OrderBySubClause(_orderByClauses);
        configureOrderBy(orderByBuilder);
        orderByBuilder.End();
        return this;
    }

    /// <summary>
    /// RETURN clause <br/>
    /// Sample: RETURN n.name
    /// </summary>
    /// <param name="configureReturn"></param>
    /// <returns></returns>
    public IReturnQuery Return(Action<ReturnClause> configureReturn)
    {
        var returnBuilder = new ReturnClause(_returnClauses);
        configureReturn(returnBuilder);
        returnBuilder.End();
        return this;
    }

    /// <summary>
    /// WHERE sub-clause <br/>
    /// Sample: WHERE n.name = 'Peter'
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public IWhereSubQuery Where(Action<WhereSubClause> conditions)
    {
        var whereBuilder = new WhereSubClause(_whereClauses);
        conditions(whereBuilder);
        whereBuilder.End();
        return this;
    }

    /// <summary>
    /// CREATE clause <br/>
    /// Sample: CREATE (n:Person)
    /// </summary>
    /// <param name="configureCreate"></param>
    /// <returns></returns>
    public ICreateQuery Create(Action<CreateClause> configureCreate)
    {
        var createBuilder = new CreateClause(_createClauses);
        configureCreate(createBuilder);
        createBuilder.End();
        return this;
    }

    /// <summary>
    /// MATCH clause <br/>
    /// Sample: MATCH (movie:Movie)
    /// </summary>
    /// <param name="configureMatch"></param>
    /// <returns></returns>
    public IMatchQuery Match(Action<MatchClause> configureMatch)
    {
        var matchBuilder = new MatchClause(_matchClauses);
        configureMatch(matchBuilder);
        matchBuilder.End();
        return this;
    }

    /// <summary>
    /// OPTIONAL MATCH clause does as MATCH <br/>
    /// Sample: OPTIONAL MATCH (p)-[r:DIRECTED]->()
    /// </summary>
    /// <param name="configureOptMatch"></param>
    /// <returns></returns>
    public IOptMatchQuery OptionalMatch(Action<OptMatchClause> configureOptMatch)
    {
        var optMatchBuilder = new OptMatchClause(_optMatchClauses);
        configureOptMatch(optMatchBuilder);
        optMatchBuilder.End();
        return this;
    }
    
    /// <summary>
    /// DELETE clause is used to delete nodes, relationships or paths <br/>
    /// Sample: DELETE r
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    public IDeleteQuery Delete(Action<DeleteClause> configureDelete)
    {
        var deleteBuilder = new DeleteClause(_deleteClauses, false);
        configureDelete(deleteBuilder);
        deleteBuilder.End();
        return this;
    }

    /// <summary>
    /// DETACH DELETE clause may not be permitted to users with restricted security privileges <br/>
    /// Sample: DETACH DELETE n
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    public IDeleteQuery DetachDelete(Action<DeleteClause> configureDelete)
    {
        var deleteBuilder = new DeleteClause(_deleteClauses, true);
        configureDelete(deleteBuilder);
        deleteBuilder.End();
        return this;
    }
    
    /// <summary>
    /// Generate Cypher query
    /// </summary>
    /// <returns>string</returns>
    public string Build()
    {
        StringBuilder queryBuilder = new();
        var clauses = new List<StringBuilder>
        {
            _matchClauses,
            _optMatchClauses,
            _whereClauses,
            _createClauses,
            _deleteClauses,
            _returnClauses,
            _orderByClauses
        };
        foreach (var clause in clauses)
        {
            AppendClause(clause, queryBuilder);
        }

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