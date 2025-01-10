using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IMatchQuery
{
    IOptMatchQuery Match(Action<MatchBuilder> configureMatch);
    IOrderBySubQuery Select(Action<SelectBuilder> configureReturn);
    ISelectQuery OptionalMatch(Action<OptMatchBuilder> configureOptMatch);
    IBuildQuery Create(Action<CreateBuilder> configureCreate);
    IBuildQuery Delete(Action<DeleteBuilder> configureDelete);
    IBuildQuery DetachDelete(Action<DeleteBuilder> configureDelete);
}