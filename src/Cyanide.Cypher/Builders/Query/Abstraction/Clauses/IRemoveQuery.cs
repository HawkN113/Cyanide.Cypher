using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query;

public interface IRemoveQuery: IReturnQuery
{
    /// <summary>
    /// The REMOVE clause is used to remove properties from nodes and relationships, and to remove labels from nodes. <br/>
    /// Sample: REMOVE n.Name
    /// </summary>
    /// <param name="configureRemove"></param>
    /// <returns></returns>
    IRemoveQuery Remove(Action<RemoveClause> configureRemove);
}