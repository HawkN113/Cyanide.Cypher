using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface IReturnQuery: IOrderBySubQuery
{
    /// <summary>
    /// RETURN clause <br/>
    /// Sample: RETURN n.name
    /// </summary>
    /// <param name="configureReturn"></param>
    /// <returns></returns>
    IReturnQuery Return(Action<ReturnClause> configureReturn);
}