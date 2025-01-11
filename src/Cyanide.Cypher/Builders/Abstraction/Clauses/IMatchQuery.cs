using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IMatchQuery
{
    IOptMatchQuery Match(Action<MatchClause> configureMatch);
    IOrderBySubQuery Return(Action<ReturnClause> configureReturn);
    IReturnQuery OptionalMatch(Action<OptMatchClause> configureOptMatch);
    IBuildQuery Create(Action<CreateClause> configureCreate);
    IBuildQuery Delete(Action<DeleteClause> configureDelete);
    IBuildQuery DetachDelete(Action<DeleteClause> configureDelete);
}