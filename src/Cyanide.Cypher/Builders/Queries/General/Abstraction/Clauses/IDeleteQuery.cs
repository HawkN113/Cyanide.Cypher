using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface IDeleteQuery: IBuildQuery
{
    /// <summary>
    /// DELETE clause is used to delete nodes, relationships or paths <br/>
    /// Sample: DELETE r
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    IDeleteQuery Delete(Action<DeleteClause> configureDelete);
}