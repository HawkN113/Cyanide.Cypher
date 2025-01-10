namespace Cyanide.Cypher.Builders;

public sealed class Entity(string type, string? alias = null, Field[]? properties = null)
{
    public string Type { get; } = type;
    public string? Alias { get; } = alias;
    public Field[]? Properties { get; } = properties;
}