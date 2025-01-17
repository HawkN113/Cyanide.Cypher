namespace Cyanide.Cypher.Builders.Abstraction.Fields;

public interface IFieldProperty<out T> where T : class
{
    T WithField(string fieldName, string fieldAlias);
}