using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IBuildQuery
{
    IOrderBySubQuery Return(Action<ReturnClause> configureReturn);
    string Build();
}