using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query.Abstraction.Clauses;

public interface ISetClause: IReturnQuery
{
    /// <summary>
    /// The SET clause is used to update labels on nodes and properties on nodes and relationships <br/>
    /// Sample: SET n.Name <br/>
    /// Sample: SET n.Name = 'Neo'
    /// </summary>
    /// <param name="configureSet"></param>
    /// <returns></returns>
    ISetClause Set(Action<SetClause> configureSet);
}