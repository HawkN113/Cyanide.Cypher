using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query.Abstraction.Clauses;

public interface IDeleteQuery: IBuildQuery
{
    /// <summary>
    /// DELETE clause is used to delete nodes, relationships or paths <br/>
    /// Sample: DELETE r
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    IDeleteQuery Delete(Action<DeleteClause> configureDelete);
}