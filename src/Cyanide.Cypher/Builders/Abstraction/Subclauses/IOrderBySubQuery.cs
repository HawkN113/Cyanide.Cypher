using Cyanide.Cypher.Builders.Abstraction.Clauses;

namespace Cyanide.Cypher.Builders.Abstraction.Subclauses;

public interface IOrderBySubQuery
{
    IBuildQuery OrderBy(Action<OrderBySubClause> configureOrderBy);
    string Build();
}