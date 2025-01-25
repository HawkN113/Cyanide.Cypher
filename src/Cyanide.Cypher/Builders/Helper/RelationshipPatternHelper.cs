using System.Text;

namespace Cyanide.Cypher.Builders.Helper;

internal static class RelationshipPatternHelper
{
    public static string Create(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
    {
        var label = string.IsNullOrWhiteSpace(alias) ? $"[:{type}]" : $"[{alias}:{type}]";

        return relation switch
        {
            RelationshipType.Direct => $"-{label}->",
            RelationshipType.InDirect => $"<-{label}-",
            RelationshipType.UnDirect => $"<-{label}->",
            RelationshipType.BiDirect => $"->{label}<-",
            _ => $"-{label}-"
        };
    }

    public static string Create(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null, Entity? right = null)
    {
        var label = GetRelationLabel(entity, null);
        
        if (entity.Properties is { Length: > 0 })
        {
            var properties = string.Join(", ", entity.Properties.Select(p => $"{p.Label}: {p.Value}"));
            label = GetRelationLabel(entity, new StringBuilder(properties));
        }
        
        var leftLabel = left != null ? string.Join("", NodePatternBuilder.CreateNode(left)) : string.Empty;
        var rightLabel = right != null ? string.Join("", NodePatternBuilder.CreateNode(right)) : string.Empty;

        return relation switch
        {
            RelationshipType.Direct => $"{leftLabel}-{label}->{rightLabel}",
            RelationshipType.InDirect => $"{leftLabel}<-{label}-{rightLabel}",
            RelationshipType.UnDirect => $"{leftLabel}<-{label}->{rightLabel}",
            RelationshipType.BiDirect => $"{leftLabel}->{label}<-{rightLabel}",
            _ => $"{leftLabel}-{label}-{rightLabel}"
        };
    }

    public static string Create(Entity left, Entity right, BasicRelationshipType relation = BasicRelationshipType.RelatedTo)
    {
        var leftLabel = string.Join("", NodePatternBuilder.CreateNode(left));
        var rightLabel = string.Join("", NodePatternBuilder.CreateNode(right));

        return relation switch
        {
            BasicRelationshipType.Directed => $"{leftLabel}-->{rightLabel}",
            BasicRelationshipType.InDirected => $"{leftLabel}<--{rightLabel}",
            _ => $"{leftLabel}--{rightLabel}"
        };
    }

    private static string GetRelationLabel(Entity entity, StringBuilder? properties)
    {
        return entity.Alias switch
        {
            null => properties is null ? $"[{entity.Type}]" : $"[{entity.Type} {{{properties}}}]",
            "" => properties is null ? $"[:{entity.Type}]" : $"[:{entity.Type} {{{properties}}}]",
            _ => properties is null
                ? $"[{entity.Alias}:{entity.Type}]"
                : $"[{entity.Alias}:{entity.Type} {{{properties}}}]"
        };
    }
}