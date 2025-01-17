namespace Cyanide.Cypher.Builders.Abstraction.Fields;

public interface IFieldAlias<out T> where T : class
{
    T WithField(string fieldName, string fieldAlias, string aliasName);
    T WithField(string fieldAlias);
}