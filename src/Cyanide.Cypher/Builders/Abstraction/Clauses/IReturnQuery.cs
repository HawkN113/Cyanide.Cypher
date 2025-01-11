using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IReturnQuery
{
    IOrderBySubQuery Return(Action<ReturnClause> configureReturn);
    string Build();
}