using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IMatchQuery: IOptMatchQuery, IDeleteQuery, IRemoveQuery, ISetClause, IWithQuery
{
    /// <summary>
    /// MATCH clause <br/>
    /// Sample: MATCH (movie:Movie)
    /// </summary>
    /// <param name="configureMatch"></param>
    /// <returns></returns>
    IMatchQuery Match(Action<MatchClause> configureMatch);
}