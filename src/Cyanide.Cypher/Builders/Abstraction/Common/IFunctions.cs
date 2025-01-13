namespace Cyanide.Cypher.Builders.Abstraction.Common;

public interface IFunctionCount<out T> where T : class
{
    T Count(string fieldName, string fieldAlias, string aliasName);
}
public interface IFunctionToUpper<out T> where T : class
{
    T ToUpper(string fieldName, string fieldAlias, string aliasName);
}