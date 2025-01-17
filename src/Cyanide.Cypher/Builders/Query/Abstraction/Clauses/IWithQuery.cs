using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query.Abstraction.Clauses;

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