using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query;

public interface IDetachDeleteQuery: IBuildQuery, IBaseQuery
{
    /// <summary>
    /// DETACH DELETE clause may not be permitted to users with restricted security privileges <br/>
    /// Sample: DETACH DELETE n
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    IDetachDeleteQuery DetachDelete(Action<DetachDeleteClause> configureDelete);
}