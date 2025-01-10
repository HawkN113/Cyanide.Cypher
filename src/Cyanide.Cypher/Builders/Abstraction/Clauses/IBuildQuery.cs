using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IBuildQuery
{
    IOrderBySubQuery Select(Action<SelectBuilder> configureReturn);
    string Build();
}