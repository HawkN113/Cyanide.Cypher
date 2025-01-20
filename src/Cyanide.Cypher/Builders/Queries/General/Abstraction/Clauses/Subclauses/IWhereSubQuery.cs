using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

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
