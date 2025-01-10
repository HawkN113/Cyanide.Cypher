using Cyanide.Cypher.Builders.Abstraction.Clauses;

namespace Cyanide.Cypher.Builders.Abstraction.Subclauses;

public interface IWhereSubQuery
{
    IMatchQuery Where(Action<WhereBuilder> conditions);
}