namespace Cyanide.Cypher.Builders.Abstraction.Functions;

public interface IToUpper<out T> where T : class
{
    T ToUpper(string fieldName, string fieldAlias, string aliasName);
}