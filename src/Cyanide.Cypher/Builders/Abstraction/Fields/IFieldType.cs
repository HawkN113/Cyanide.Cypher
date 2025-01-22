namespace Cyanide.Cypher.Builders.Abstraction.Fields;

public interface IFieldType<out T> where T : class
{
    T WithType(string alias);
    T WithType(string alias, string aliasName);
}