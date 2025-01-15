using System.Text;
using Cyanide.Cypher.Builders.Query;
using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders;

internal sealed class CypherQueryBuilder : IQuery, IMatchQuery
{
    private readonly Dictionary<string, StringBuilder> _clauses = new()
    {
        { "MATCH", new StringBuilder() },
        { "OPTIONAL_MATCH", new StringBuilder() },
        { "WHERE", new StringBuilder() },
        { "CREATE", new StringBuilder() },
        { "DELETE", new StringBuilder() },
        { "DETACH_DELETE", new StringBuilder() },
        { "REMOVE", new StringBuilder() },
        { "SET", new StringBuilder() },
        { "WITH", new StringBuilder() },
        { "RETURN", new StringBuilder() },
        { "ORDER_BY", new StringBuilder() },
        { "SKIP", new StringBuilder() },
        { "LIMIT", new StringBuilder() }
    };

    /// <summary>
    /// The WITH clause allows query parts to be chained together, piping the results from one to be used as starting points <br/>
    /// Sample: WITH otherPerson, count(*) AS foaf
    /// </summary>
    /// <param name="configureWith"></param>
    /// <returns></returns>
    public IWithQuery With(Action<WithClause> configureWith)
    {
        ConfigureClause("WITH", configureWith);
        return this;
    }
    
    /// <summary>
    /// ORDER BY is a sub-clause following RETURN <br/>
    /// Sample: ORDER BY n.name
    /// </summary>
    /// <param name="configureOrderBy"></param>
    /// <returns></returns>
    public IOrderBySubQuery OrderBy(Action<OrderBySubClause> configureOrderBy)
    {
        ConfigureClause("ORDER_BY", configureOrderBy);
        return this;
    }

    /// <summary>
    /// LIMIT constrains the number of returned rows <br/>
    /// Sample: LIMIT 1 + toInteger(3 * rand())
    /// Sample: LIMIT 3
    /// </summary>
    /// <param name="configureLimit"></param>
    /// <returns></returns>
    public ILimitClause Limit(Action<LimitClause> configureLimit)
    {
        ConfigureClause("LIMIT", configureLimit);
        return this;
    }
    
    /// <summary>
    /// SKIP defines from which row to start including the rows in the output <br/>
    /// Sample: SKIP 1 + toInteger(3 * rand())
    /// Sample: SKIP 3
    /// </summary>
    /// <param name="configureSkip"></param>
    /// <returns></returns>
    public ISkipClause Skip(Action<SkipClause> configureSkip)
    {
        ConfigureClause("SKIP", configureSkip);
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
        ConfigureClause("RETURN", configureReturn);
        return this;
    }

    /// <summary>
    /// The SET clause is used to update labels on nodes and properties on nodes and relationships <br/>
    /// Sample: SET n.Name <br/>
    /// Sample: SET n.Name = 'Neo'
    /// </summary>
    /// <param name="configureSet"></param>
    /// <returns></returns>
    public ISetClause Set(Action<SetClause> configureSet)
    {
        ConfigureClause("SET", configureSet);
        return this;
    }

    /// <summary>
    /// The REMOVE clause is used to remove properties from nodes and relationships, and to remove labels from nodes. <br/>
    /// Sample: REMOVE n.Name
    /// </summary>
    /// <param name="configureRemove"></param>
    /// <returns></returns>
    public IRemoveQuery Remove(Action<RemoveClause> configureRemove)
    {
        ConfigureClause("REMOVE", configureRemove);
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
        ConfigureClause("WHERE", conditions);
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
        ConfigureClause("CREATE", configureCreate);
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
        ConfigureClause("MATCH", configureMatch);
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
        ConfigureClause("OPTIONAL_MATCH", configureOptMatch);
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
        ConfigureClause("DELETE", configureDelete);
        return this;
    }

    /// <summary>
    /// DETACH DELETE clause may not be permitted to users with restricted security privileges <br/>
    /// Sample: DETACH DELETE n
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    public IDetachDeleteQuery DetachDelete(Action<DetachDeleteClause> configureDelete)
    {
        ConfigureClause("DETACH_DELETE", configureDelete);
        return this;
    }
    
    /// <summary>
    /// Generates the final Cypher query by concatenating all configured clauses.
    /// </summary>
    /// <returns>The fully constructed Cypher query as a string.</returns>
    public string Build()
    {
        return string.Join(" ", _clauses.Values
            .Where(clause => clause.Length > 0)
            .Select(clause => clause.ToString().Trim()));
    }
    
    private void ConfigureClause<T>(string key, Action<T> configure) where T : class, new()
    {
        if (!_clauses.TryGetValue(key, out var clauseBuilder)) return;
        var builder = Activator.CreateInstance(typeof(T), clauseBuilder) as T;
        configure(builder!);
        (builder as dynamic)?.End();
    }
}