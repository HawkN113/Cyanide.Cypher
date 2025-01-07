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
    
    public static List<string> Node(Entity entity, Property property)
    {
        List<string> patterns = [];
        patterns.Add($"(:{entity.Type} {{{property.Label}: {property.Value}}})");
        return patterns;
    }

    public static List<string> Node(Entity entity, Property[]? properties = null)
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

            patterns.Add(!string.IsNullOrWhiteSpace(entity.Alias)
                ? $"({entity.Alias}:{entity.Type} {{{result}}})"
                : $"(:{entity.Type} {{{result}}})");
        }
        else
        {
            patterns.Add(!string.IsNullOrWhiteSpace(entity.Alias)
                ? $"({entity.Alias}:{entity.Type})"
                : $"({entity.Type})");
        }

        return patterns;
    }
}