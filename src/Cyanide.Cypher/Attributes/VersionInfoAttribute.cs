namespace Cyanide.Cypher.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
public class VersionInfoAttribute(string version) : Attribute
{
    public string version { get; } = version;
}