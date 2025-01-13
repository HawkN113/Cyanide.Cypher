namespace Cyanide.Cypher.Builders.Abstraction;

public interface IBuildQuery
{
    /// <summary>
    /// Generate Cypher query
    /// </summary>
    /// <returns>string</returns>
    string Build();
}