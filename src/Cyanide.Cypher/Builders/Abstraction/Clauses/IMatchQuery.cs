using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IMatchQuery
{
    IOptMatchQuery Match(Func<MatchBuilder, MatchBuilder> configureMatch);
    IOrderBySubQuery Select(Func<SelectBuilder, SelectBuilder> configureReturn);
    ISelectQuery OptionalMatch(Func<OptMatchBuilder, OptMatchBuilder> configureOptMatch);
    IBuildQuery Create(Func<CreateBuilder, CreateBuilder> configureCreate);
    IBuildQuery Delete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
    IBuildQuery DetachDelete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
}