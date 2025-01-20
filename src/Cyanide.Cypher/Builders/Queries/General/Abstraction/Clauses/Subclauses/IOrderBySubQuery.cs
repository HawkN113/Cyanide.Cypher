using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface IOrderBySubQuery : ISkipClause
{
    /// <summary>
    /// ORDER BY is a sub-clause following RETURN <br/>
    /// Sample: ORDER BY n.name
    /// </summary>
    /// <param name="configureOrderBy"></param>
    /// <returns></returns>
    IOrderBySubQuery OrderBy(Action<OrderBySubClause> configureOrderBy);
}