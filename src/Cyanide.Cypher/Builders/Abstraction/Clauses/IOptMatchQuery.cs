using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IOptMatchQuery
{
    ISelectQuery OptionalMatch(Action<OptMatchBuilder> configureOptMatch);
    IOrderBySubQuery Select(Action<SelectBuilder> configureReturn);
    IMatchQuery Match(Action<MatchBuilder> configureMatch);
    IMatchQuery Where(Action<WhereBuilder> conditions);
    IBuildQuery Create(Action<CreateBuilder> configureCreate);
    IBuildQuery Delete(Action<DeleteBuilder> configureDelete);
    IBuildQuery DetachDelete(Action<DeleteBuilder> configureDelete);
}