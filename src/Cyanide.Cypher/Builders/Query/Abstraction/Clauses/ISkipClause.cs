using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query.Abstraction.Clauses;

public interface ISkipClause: ILimitClause
{
    /// <summary>
    /// SKIP defines from which row to start including the rows in the output <br/>
    /// Sample: SKIP 3 <br/>
    /// Sample: SKIP 1 + toInteger(3 * rand())
    /// </summary>
    /// <param name="configureSkip"></param>
    /// <returns></returns>
    ISkipClause Skip(Action<SkipClause> configureSkip);
}