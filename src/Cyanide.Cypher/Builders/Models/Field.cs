namespace Cyanide.Cypher.Builders;

public sealed class Field(string label, string value)
{
    /// <summary>
    /// Property name
    /// </summary>
    public string Label { get; } = label;
    
    /// <summary>
    /// Property value
    /// </summary>
    public string Value { get; } = value;
}