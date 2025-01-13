using Cyanide.Cypher.Builders.Admin;
using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher;

/// <summary>
/// Provides factory for creating Cypher query objects.
/// </summary>
public static class Factory
{
    /// <summary>
    /// Creates and returns a new instance of a Cypher query builder.
    /// Use this to construct Cypher queries programmatically.
    /// </summary>
    /// <returns>An instance of <see cref="IQuery"/> for building Cypher queries.</returns>
    public static IQuery QueryBuilder()
    {
        return new CypherQueryBuilder();
    }
    
    /// <summary>
    /// Creates and returns a new instance of a Cypher query builder (Admin management).
    /// Use this to construct admin Cypher queries programmatically.
    /// </summary>
    /// <returns>An instance of <see cref="IQuery"/> for building Cypher queries.</returns>
    public static IAdminQuery AdminQueryBuilder()
    {
        return new CypherAdminBuilder();
    }
}