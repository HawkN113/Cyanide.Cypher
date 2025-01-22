using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface IDetachDeleteQuery: IBuildQuery
{
    /// <summary>
    /// DETACH DELETE clause may not be permitted to users with restricted security privileges <br/>
    /// Sample: DETACH DELETE n
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    IDetachDeleteQuery DetachDelete(Action<DetachDeleteClause> configureDelete);
}