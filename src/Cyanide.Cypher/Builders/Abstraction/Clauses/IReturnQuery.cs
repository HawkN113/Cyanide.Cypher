using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IReturnQuery: IOrderBySubQuery
{
    IReturnQuery Return(Action<ReturnClause> configureReturn);
}