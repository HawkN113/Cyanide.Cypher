using System.Text;

namespace Cyanide.Cypher.Builders.Helper;

internal static class NodeHelper
{
    public static List<string> EmptyNode()
    {
        List<string> patterns =
        [
            "()"
        ];
        return patterns;
    }
    
    public static List<string> Node(string type, Property property)
    {
        List<string> patterns = [];
        patterns.Add($"(:{type} {{{property.Label}: {property.Value}}})");
        return patterns;
    }

    public static List<string> Node(string type, string alias = "", Property[]? properties = null)
    {
        List<string> patterns = [];
        if (properties is not null && properties.Any())
        {
            StringBuilder result = new();
            foreach (var property in properties)
            {
                result.Append($"{property.Label}: {property.Value}");
                if (property != properties[^1])
                    result.Append(", ");
            }

            patterns.Add(!string.IsNullOrWhiteSpace(alias)
                ? $"({alias}:{type} {{{result}}})"
                : $"(:{type} {{{result}}})");
        }
        else
        {
            patterns.Add(!string.IsNullOrWhiteSpace(alias)
                ? $"({alias}:{type})"
                : $"({type})");
        }

        return patterns;
    }
}