using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IOptMatchQuery
{
    IReturnQuery OptionalMatch(Action<OptMatchClause> configureOptMatch);
    IOrderBySubQuery Return(Action<ReturnClause> configureReturn);
    IMatchQuery Match(Action<MatchClause> configureMatch);
    IMatchQuery Where(Action<WhereSubClause> conditions);
    IBuildQuery Create(Action<CreateClause> configureCreate);
    IBuildQuery Delete(Action<DeleteClause> configureDelete);
    IBuildQuery DetachDelete(Action<DeleteClause> configureDelete);
}