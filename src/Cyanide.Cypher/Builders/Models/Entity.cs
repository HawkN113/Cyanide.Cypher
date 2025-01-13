namespace Cyanide.Cypher.Builders;

/// <summary>
/// Node (Entity)
/// </summary>
/// <param name="type"></param>
/// <param name="alias"></param>
/// <param name="properties"></param>
public sealed class Entity(string type, string? alias = null, Field[]? properties = null)
{
    /// <summary>
    /// Entity type <br/>
    /// Sample: Person
    /// </summary>
    public string Type { get; } = type;
    
    /// <summary>
    /// Alias name of the entity <br/>
    /// Sample: n.Person
    /// </summary>
    public string? Alias { get; } = alias;
    
    /// <summary>
    /// Fields/Properties of the entity <br/>
    /// </summary>
    public Field[]? Properties { get; } = properties;
}