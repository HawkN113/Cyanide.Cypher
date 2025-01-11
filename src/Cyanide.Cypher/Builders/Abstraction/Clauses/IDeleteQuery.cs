using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IDeleteQuery: IBuildQuery
{
    /// <summary>
    /// DELETE clause is used to delete nodes, relationships or paths <br/>
    /// Sample: DELETE r
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    IDeleteQuery Delete(Action<DeleteClause> configureDelete);
    
    /// <summary>
    /// DETACH DELETE clause may not be permitted to users with restricted security privileges <br/>
    /// Sample: DETACH DELETE n
    /// </summary>
    /// <param name="configureDelete"></param>
    /// <returns></returns>
    IDeleteQuery DetachDelete(Action<DeleteClause> configureDelete);
}