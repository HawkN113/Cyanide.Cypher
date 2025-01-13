using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IWithQuery: IReturnQuery
{
    /// <summary>
    /// RETURN clause <br/>
    /// Sample: RETURN n.name
    /// </summary>
    /// <param name="configureWith"></param>
    /// <returns></returns>
    IWithQuery With(Action<WithClause> configureWith);
}