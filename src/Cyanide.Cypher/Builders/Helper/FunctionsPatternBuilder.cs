namespace Cyanide.Cypher.Builders.Helper;

internal static class FunctionsPatternBuilder
{
    public static string Count(string fieldName, string? fieldAlias, string aliasName)
    {
        return fieldAlias is null ? $"count({fieldName}) AS {aliasName}" : $"count({fieldAlias}.{fieldName}) AS {aliasName}";
    }
    
    public static string ToUpper(string fieldName, string? fieldAlias, string aliasName)
    {
        return $"toUpper({fieldAlias}.{fieldName}) AS {aliasName}";
    }
}