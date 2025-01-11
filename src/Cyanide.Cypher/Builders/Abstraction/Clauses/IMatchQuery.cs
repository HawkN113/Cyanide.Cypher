namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IMatchQuery: IOptMatchQuery, IDeleteQuery
{
    IMatchQuery Match(Action<MatchClause> configureMatch);
}