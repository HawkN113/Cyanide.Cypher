using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface IReturnQuery: IOrderBySubQuery, IUnionClause
{
    /// <summary>
    /// RETURN clause <br/>
    /// Sample: RETURN n.name
    /// </summary>
    /// <param name="configureReturn"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    IReturnQuery Return(Action<ReturnClause> configureReturn);
}