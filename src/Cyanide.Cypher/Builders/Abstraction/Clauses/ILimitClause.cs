using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface ILimitClause: IBuildQuery
{
    /// <summary>
    /// LIMIT constrains the number of returned rows <br/>
    /// Sample: LIMIT 3e <br/>
    /// Sample: LIMIT 1 + toInteger(3 * rand())
    /// </summary>
    /// <param name="configureLimit"></param>
    /// <returns></returns>
    ILimitClause Limit(Action<LimitClause> configureLimit);
}