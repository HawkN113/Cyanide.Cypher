using Cyanide.Cypher.Attributes;

namespace Cyanide.Cypher.Builders.Abstraction;

public interface IBuildQuery
{
    /// <summary>
    /// Generate Cypher query
    /// </summary>
    /// <returns>string</returns>
    [VersionInfo("4.4")]
    string Build();
}