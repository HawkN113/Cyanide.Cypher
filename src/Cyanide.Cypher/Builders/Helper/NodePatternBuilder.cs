using System.Text;

namespace Cyanide.Cypher.Builders.Helper;

internal static class NodePatternBuilder
{
    public static List<string> CreateEmptyNode()
    {
        return ["()"];
    }

    public static List<string> CreateNode(Entity entity)
    {
        var patterns = new List<string>();
        if (entity.Properties is { Length: > 0 })
        {
            var properties = string.Join(", ", entity.Properties.Select(p => $"{p.Label}: {p.Value}"));
            patterns.AddRange(GetEntityLabel(entity, new StringBuilder(properties)));
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
        return
        [
            entity.Alias switch
            {
                null => $"({entity.Type} {{{properties}}})",
                "" => $"(:{entity.Type} {{{properties}}})",
                _ => $"({entity.Alias}:{entity.Type} {{{properties}}})"
            }
        ];
    }
}