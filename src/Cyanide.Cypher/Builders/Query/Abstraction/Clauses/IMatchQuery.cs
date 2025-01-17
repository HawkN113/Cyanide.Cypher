using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query.Abstraction.Clauses;

public interface IMatchQuery: IOptMatchQuery, IDeleteQuery, IDetachDeleteQuery, IRemoveQuery, ISetClause, IWithQuery
{
    /// <summary>
    /// MATCH clause <br/>
    /// Sample: MATCH (movie:Movie)
    /// </summary>
    /// <param name="configureMatch"></param>
    /// <returns></returns>
    IMatchQuery Match(Action<MatchClause> configureMatch);
}