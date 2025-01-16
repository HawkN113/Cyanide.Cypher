using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query;

public interface IOptMatchQuery: IWhereSubQuery,IBaseQuery
{
    /// <summary>
    /// OPTIONAL MATCH clause does as MATCH <br/>
    /// Sample: OPTIONAL MATCH (p)-[r:DIRECTED]->()
    /// </summary>
    /// <param name="configureOptMatch"></param>
    /// <returns></returns>
    IOptMatchQuery OptionalMatch(Action<OptMatchClause> configureOptMatch);
}