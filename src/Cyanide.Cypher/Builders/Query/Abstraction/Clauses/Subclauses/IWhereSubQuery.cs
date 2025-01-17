using Cyanide.Cypher.Builders.Query.Commands.Subclauses;

namespace Cyanide.Cypher.Builders.Query.Abstraction.Clauses.Subclauses;

public interface IWhereSubQuery : ICreateQuery
{
    /// <summary>
    /// WHERE sub-clause <br/>
    /// Sample: WHERE n.name = 'Peter'
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    IWhereSubQuery Where(Action<WhereSubClause> conditions);
}
