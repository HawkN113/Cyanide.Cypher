namespace Cyanide.Cypher.Builders.Abstraction;

public interface IFieldProperty<out T> where T : class
{
    T WithField(string fieldName, string fieldAlias);
}
public interface IFieldAlias<out T> where T : class
{
    T WithField(string fieldName, string fieldAlias, string aliasName);
    T WithField(string fieldAlias);
}