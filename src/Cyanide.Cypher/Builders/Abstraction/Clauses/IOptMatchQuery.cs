using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IOptMatchQuery
{
    ISelectQuery OptionalMatch(Func<OptMatchBuilder, OptMatchBuilder> configureOptMatch);
    IOrderBySubQuery Select(Func<SelectBuilder, SelectBuilder> configureReturn);
    IMatchQuery Match(Func<MatchBuilder, MatchBuilder> configureMatch);
    IMatchQuery Where(Func<WhereBuilder, WhereBuilder> conditions);
    IBuildQuery Create(Func<CreateBuilder, CreateBuilder> configureCreate);
    IBuildQuery Delete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
    IBuildQuery DetachDelete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
}