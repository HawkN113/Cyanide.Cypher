using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface ISelectQuery
{
    IOrderBySubQuery Return(Action<ReturnClause> configureReturn);
    string Build();
}