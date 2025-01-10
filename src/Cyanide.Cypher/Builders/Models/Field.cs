namespace Cyanide.Cypher.Builders;

public sealed class Field(string label, string value)
{
    public string Label { get; } = label;
    public string Value { get; } = value;
}