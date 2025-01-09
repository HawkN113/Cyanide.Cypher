using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IBuildQuery
{
    IOrderBySubQuery Select(Func<SelectBuilder, SelectBuilder> configureReturn);
    string Build();
}