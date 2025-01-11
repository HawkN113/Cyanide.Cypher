using Cyanide.Cypher.Builders.Abstraction.Clauses;

namespace Cyanide.Cypher.Builders.Abstraction.Subclauses;

public interface IOrderBySubQuery: IBuildQuery
{
    IOrderBySubQuery OrderBy(Action<OrderBySubClause> configureOrderBy);
}