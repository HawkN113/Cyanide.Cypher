using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IOptMatchQuery: IWhereSubQuery
{
    IOptMatchQuery OptionalMatch(Action<OptMatchClause> configureOptMatch);
}