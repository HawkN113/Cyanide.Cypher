namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IStartQuery
{
    IMatchQuery Match(Action<MatchClause> configureMatch);
    ICreateQuery Create(Action<CreateClause> configureCreate);
}