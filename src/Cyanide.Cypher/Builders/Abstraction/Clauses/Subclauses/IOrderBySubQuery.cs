using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses.Subclauses;

public interface IOrderBySubQuery: ILimitClause
{
    /// <summary>
    /// ORDER BY ORDER BY is a sub-clause following RETURN <br/>
    /// Sample: ORDER BY n.name
    /// </summary>
    /// <param name="configureOrderBy"></param>
    /// <returns></returns>
    IOrderBySubQuery OrderBy(Action<OrderBySubClause> configureOrderBy);
}