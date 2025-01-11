using Cyanide.Cypher.Builders.Abstraction.Clauses;

namespace Cyanide.Cypher.Builders.Abstraction.Subclauses;

public interface IWhereSubQuery: ICreateQuery
{
    IWhereSubQuery Where(Action<WhereSubClause> conditions);
}