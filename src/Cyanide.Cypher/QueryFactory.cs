using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Abstraction.Clauses;

namespace Cyanide.Cypher;

public static class Factory
{
    public static IQuery QueryBuilder()
    {
        return new CypherQueryBuilder();
    }
}