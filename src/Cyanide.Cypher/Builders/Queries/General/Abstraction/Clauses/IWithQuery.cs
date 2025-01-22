using Cyanide.Cypher.Attributes;
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
    [VersionInfo("4.4")]
    IWithQuery With(Action<WithClause> configureWith);
}