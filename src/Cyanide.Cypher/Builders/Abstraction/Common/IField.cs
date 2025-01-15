namespace Cyanide.Cypher.Builders.Abstraction.Common;

public interface IFieldProperty<out T> where T : class
{
    T WithField(string fieldName, string fieldAlias);
}
public interface IFieldAlias<out T> where T : class
{
    T WithField(string fieldName, string fieldAlias, string aliasName);
    T WithField(string fieldAlias);
}
public interface IFieldType<out T> where T : class
{
    T WithType(string alias);
    T WithType(string alias, string aliasName);
}