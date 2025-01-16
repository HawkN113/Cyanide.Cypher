﻿using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Query;
using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders;

internal enum ClauseKeys
{
    Match = 0,
    OptionalMatch = 1,
    Where = 2,
    Create = 3,
    Delete = 4,
    DetachDelete = 5,
    Remove = 6,
    Set = 7,
    With = 8,
    Return = 9,
    OrderBy = 10,
    Skip = 11,
    Limit = 12
}

internal sealed class CypherQueryBuilder : IQuery, IMatchQuery
{
    private readonly Dictionary<string, StringBuilder> _clauses;
    public CypherQueryBuilder()
    {
        _clauses = Enum.GetValues<ClauseKeys>().ToDictionary(
            key =>
                key.ToString(),
            _ => new StringBuilder());
    }

    /// <summary>
    /// The WITH clause allows query parts to be chained together, piping the results from one to be used as starting points <br/>
    /// Sample: WITH otherPerson, count(*) AS foaf
    /// </summary>
    /// <param name="configureWith"></param>
    /// <returns></returns>
    public IWithQuery With(Action<WithClause> configureWith) =>
        ConfigureAndReturn<IWithQuery, WithClause>(ClauseKeys.With.ToString(), configureWith);

    /// <summary>
    /// ORDER BY is a sub-clause following RETURN <br/>
    /// Sample: ORDER BY n.name
    /// </summary>
    /// <param name="configureOrderBy"></param>
    /// <returns></returns>
    public IOrderBySubQuery OrderBy(Action<OrderBySubClause> configureOrderBy) =>
        ConfigureAndReturn<IOrderBySubQuery, OrderBySubClause>(ClauseKeys.OrderBy.ToString(), configureOrderBy);

    /// <summary>
    /// LIMIT constrains the number of returned rows <br/>
    /// Sample: LIMIT 1 + toInteger(3 * rand())
    /// Sample: LIMIT 3
    /// </summary>
    /// <param name="configureLimit"></param>
    /// <returns></returns>
    public ILimitClause Limit(Action<LimitClause> configureLimit) =>
        ConfigureAndReturn<ILimitClause, LimitClause>(ClauseKeys.Limit.ToString(), configureLimit);

    /// <summary>
    /// SKIP defines from which row to start including the rows in the output <br/>
    /// Sample: SKIP 1 + toInteger(3 * rand())
    /// Sample: SKIP 3
    /// </summary>
    /// <param name="configureSkip"></param>
    /// <returns></returns>
    public ISkipClause Skip(Action<SkipClause> configureSkip) =>
        ConfigureAndReturn<ISkipClause, SkipClause>(ClauseKeys.Skip.ToString(), configureSkip);

    /// <summary>
    /// RETURN clause <br/>
    /// Sample: RETURN n.name
    /// </summary>
    /// <param name="configureReturn"></param>
    /// <returns></returns>
    public IReturnQuery Return(Action<ReturnClause> configureReturn) =>
        ConfigureAndReturn<IReturnQuery, ReturnClause>(ClauseKeys.Return.ToString(), configureReturn);

    /// <summary>
    /// The SET clause is used to update labels on nodes and properties on nodes and relationships <br/>
    /// Sample: SET n.Name <br/>
    /// Sample: SET n.Name = 'Neo'
    /// </summary>
    /// <param name="configureSet"></param>
    /// <returns></returns>
    public ISetClause Set(Action<SetClause> configureSet) =>
        ConfigureAndReturn<ISetClause, SetClause>(ClauseKeys.Set.ToString(), configureSet);

    /// <summary>
    /// The REMOVE clause is used to remove properties from nodes and relationships, and to remove labels from nodes. <br/>
    /// Sample: REMOVE n.Name
    /// </summary>
    /// <param name="configureRemove"></param>
    /// <returns></returns>
    public IRemoveQuery Remove(Action<RemoveClause> configureRemove) =>
        ConfigureAndReturn<IRemoveQuery, RemoveClause>(ClauseKeys.Remove.ToString(), configureRemove);

    /// <summary>
    /// WHERE sub-clause <br/>
    /// Sample: WHERE n.name = 'Peter'
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public IWhereSubQuery Where(Action<WhereSubClause> conditions) =>
        ConfigureAndReturn<IWhereSubQuery, WhereSubClause>(ClauseKeys.Where.ToString(), conditions);

    /// <summary>
    /// CREATE clause <br/>
    /// Sample: CREATE (n:Person)
    /// </summary>
    /// <param name="configureCreate"></param>
    /// <returns></returns>
    public ICreateQuery Create(Action<CreateClause> configureCreate) =>
        ConfigureAndReturn<ICreateQuery, CreateClause>(ClauseKeys.Create.ToString(), configureCreate);

    /// <summary>
    /// MATCH clause <br/>
    /// Sample: MATCH (movie:Movie)
    /// </summary>
    /// <param name="configureMatch"></param>
    /// <returns></returns>
    public IMatchQuery Match(Action<MatchClause> configureMatch) =>
        ConfigureAndReturn<IMatchQuery, MatchClause>(ClauseKeys.Match.ToString(), configureMatch);

    /// <summary>
    /// OPTIONAL MATCH clause does as MATCH <br/>
    /// Sample: OPTIONAL MATCH (p)-[r:DIRECTED]->()
    /// </summary>
    /// <param name="configureOptMatch"></param>
    /// <returns></returns>
    public IOptMatchQuery OptionalMatch(Action<OptMatchClause> configureOptMatch) =>
        ConfigureAndReturn<IOptMatchQuery, OptMatchClause>(ClauseKeys.OptionalMatch.ToString(), configureOptMatch);

    /// <summary>
    /// DELETE clause is used to delete nodes, relationships or paths <br/>
    /// Sample: DELETE r
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    public IDeleteQuery Delete(Action<DeleteClause> configureDelete) =>
        ConfigureAndReturn<IDeleteQuery, DeleteClause>(ClauseKeys.Delete.ToString(), configureDelete);

    /// <summary>
    /// DETACH DELETE clause may not be permitted to users with restricted security privileges <br/>
    /// Sample: DETACH DELETE n
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    public IDetachDeleteQuery DetachDelete(Action<DetachDeleteClause> configureDelete) =>
        ConfigureAndReturn<IDetachDeleteQuery,DetachDeleteClause>(ClauseKeys.DetachDelete.ToString(), configureDelete);

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

    private TInterface ConfigureAndReturn<TInterface, TClause>(string key, Action<TClause> configure)
        where TClause : class, IBuilderInitializer, new() where TInterface : class
    {
        ConfigureClause(key, configure);
        if (this is not TInterface result)
            throw new InvalidOperationException(
                $"Failed to cast {nameof(CypherQueryBuilder)} to {typeof(TInterface)}");
        return result;
    }

    private void ConfigureClause<TClause>(string key, Action<TClause> configure) where TClause : class, IBuilderInitializer, new()
    {
        if (!_clauses.TryGetValue(key, out var clauseBuilder))
            throw new ArgumentException($"Invalid clause key: {key}", nameof(key));
        
        var instance = new TClause();
        
        if (instance is not IBuilderInitializer initializer) return;
        
        initializer.Initialize(clauseBuilder);
        configure(instance);
        instance.End();
    }
}