namespace Cyanide.Cypher.Builders.Abstraction.Functions;

public interface ICount<out T> where T : class
{
    T Count(string fieldName, string fieldAlias, string aliasName);
}