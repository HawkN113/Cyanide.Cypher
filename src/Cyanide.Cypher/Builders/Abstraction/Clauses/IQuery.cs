namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IQuery
{
    ICreateQuery Create(Action<CreateClause> configureCreate);
    IMatchQuery Match(Action<MatchClause> configureMatch);
    IOptMatchQuery OptionalMatch(Action<OptMatchClause> configureOptMatch);
}