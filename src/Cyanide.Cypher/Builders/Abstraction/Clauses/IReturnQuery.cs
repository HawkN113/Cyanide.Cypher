using Cyanide.Cypher.Builders.Abstraction.Clauses.Subclauses;
using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

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