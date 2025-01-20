using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

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