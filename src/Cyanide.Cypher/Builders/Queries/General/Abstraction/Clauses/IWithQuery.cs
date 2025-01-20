using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

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