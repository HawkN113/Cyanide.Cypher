namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IBuildQuery
{
    /// <summary>
    /// Generate Cypher query
    /// </summary>
    /// <returns>string</returns>
    string Build();
}