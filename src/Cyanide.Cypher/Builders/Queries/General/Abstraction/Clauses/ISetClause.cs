using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface ISetClause: IReturnQuery
{
    /// <summary>
    /// The SET clause is used to update labels on nodes and properties on nodes and relationships <br/>
    /// Sample: SET n.Name <br/>
    /// Sample: SET n.Name = 'Neo'
    /// </summary>
    /// <param name="configureSet"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    ISetClause Set(Action<SetClause> configureSet);
}