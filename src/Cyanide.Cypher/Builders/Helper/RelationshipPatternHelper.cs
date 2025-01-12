using System.Text;
using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders;

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
        var label = GetRelationLabel(entity);
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

    private static string GetRelationLabel(Entity entity)
    {
        return entity.Alias switch
        {
            null => $"[{entity.Type}]",
            "" => $"[:{entity.Type}]",
            _ => $"[{entity.Alias}:{entity.Type}]"
        };
    }
}