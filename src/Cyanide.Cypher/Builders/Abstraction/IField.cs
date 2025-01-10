namespace Cyanide.Cypher.Builders.Abstraction;

public interface IField<out T> where T : class
{
    T WithField(string fieldName, string fieldAlias, string aliasName);
    T WithField(string fieldName, string fieldAlias);
    T WithField(string fieldAlias);
}