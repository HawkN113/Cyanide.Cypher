using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface ISkipClause: ILimitClause
{
    /// <summary>
    /// SKIP defines from which row to start including the rows in the output <br/>
    /// Sample: SKIP 3 <br/>
    /// Sample: SKIP 1 + toInteger(3 * rand())
    /// </summary>
    /// <param name="configureSkip"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    ISkipClause Skip(Action<SkipClause> configureSkip);
}