using Cyanide.Cypher.Extensions;

namespace Cyanide.Cypher.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
public class EditionInfoAttribute(Edition edition) : Attribute
{
    public string edition { get; } = edition.GetDescription();
}

public enum Edition
{
    None,
    Community,
    Enterprise
}