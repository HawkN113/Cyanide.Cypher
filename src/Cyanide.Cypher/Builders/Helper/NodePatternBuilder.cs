using System.Text;
using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders;

internal static class NodePatternBuilder
{
    public static List<string> CreateEmptyNode()
    {
        List<string> patterns =
        [
            "()"
        ];
        return patterns;
    }

    public static List<string> CreateNode(Entity entity)
    {
        List<string> patterns = [];
        if (entity.Properties is not null && entity.Properties.Any())
        {
            StringBuilder result = new();
            foreach (var property in entity.Properties)
            {
                result.Append($"{property.Label}: {property.Value}");
                if (property != entity.Properties[^1])
                    result.Append(", ");
            }
            patterns.AddRange(GetEntityLabel(entity, result));
        }
        else
        {
            patterns.Add(!string.IsNullOrEmpty(entity.Alias)
                ? $"({entity.Alias}:{entity.Type})"
                : $"({entity.Type})");
        }

        return patterns;
    }

    private static List<string> GetEntityLabel(Entity entity, StringBuilder properties)
    {
        List<string> patterns = [];
        if (entity.Alias is null)
        {
            patterns.Add($"({entity.Type} {{{properties}}})");
        }
        else
        {
            patterns.Add(entity.Alias == ""
                ? $"(:{entity.Type} {{{properties}}})"
                : $"({entity.Alias}:{entity.Type} {{{properties}}})");
        }

        return patterns;
    }
}