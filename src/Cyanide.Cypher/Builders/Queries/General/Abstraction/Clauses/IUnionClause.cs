using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface IUnionClause: IBuildQuery
{
    /// <summary>
    /// UNION clause is used to combine the result of multiple queries. UNION combines the results of two or more queries into a single result set that includes all the rows that belong to any queries in the union. <br/>
    /// Sample: MATCH (n:Actor) RETURN n.name AS name UNION MATCH (n:Movie) RETURN n.title AS name
    /// </summary>
    /// <param name="configureUnion"></param>
    /// <returns></returns>
    IUnionClause Union(Action<IQuery> configureUnion);
    
    /// <summary>
    /// UNION ALL clause is used to combine the result of multiple queries. Combine two queries and retain duplicates. <br/>
    /// Sample: MATCH (n:Actor) RETURN n.name AS name UNION ALL MATCH (n:Movie) RETURN n.title AS name
    /// </summary>
    /// <param name="configureUnionAll"></param>
    /// <returns></returns>
    IUnionClause UnionAll(Action<IQuery> configureUnionAll);
}