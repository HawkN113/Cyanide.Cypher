namespace Cyanide.Cypher.Builders;

public sealed class Entity(string type, string? alias = null, Property[]? properties = null)
{
    public string Type { get; } = type;
    public string? Alias { get; } = alias;
    public Property[]? Properties { get; } = properties;
}