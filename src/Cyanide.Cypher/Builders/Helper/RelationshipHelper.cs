namespace Cyanide.Cypher.Builders.Helper;

internal static class RelationshipHelper
{
    public static string Create(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
    {
        var label = !string.IsNullOrWhiteSpace(alias) ? $"[{alias}:{type}]" : $"[:{type}]";
        var relationship = relation switch
        {
            RelationshipType.Direct => $"-{label}->",
            RelationshipType.InDirect => $"<-{label}-",
            RelationshipType.UnDirect => $"<-{label}->",
            RelationshipType.BiDirect => $"->{label}<-",
            _ => $"-{label}-"
        };
        return relationship;
    }
    
    public static string Create(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        var label = !string.IsNullOrWhiteSpace(entity.Alias) ? $"[{entity.Alias}:{entity.Type}]" : $"[:{entity.Type}]";
        var leftLabel = string.Empty;
        var rightLabel = string.Empty;

        if (left is not null)
        {
            leftLabel = left.Alias is null ? $"({left.Type})" :
                !string.Equals(left.Alias, string.Empty, StringComparison.OrdinalIgnoreCase) ? $"({left.Alias}:{left.Type})" : $"(:{left.Type})";
        }
        if (right is not null)
        {
            rightLabel = right.Alias is null ? $"({right.Type})" :
                !string.Equals(right.Alias, string.Empty, StringComparison.OrdinalIgnoreCase) ? $"({right.Alias}:{right.Type})" : $"(:{right.Type})";
        }

        var relationship = relation switch
        {
            RelationshipType.Direct => $"{leftLabel}-{label}->{rightLabel}",
            RelationshipType.InDirect => $"{leftLabel}<-{label}-{rightLabel}",
            RelationshipType.UnDirect => $"{leftLabel}<-{label}->{rightLabel}",
            RelationshipType.BiDirect => $"{leftLabel}->{label}<-{rightLabel}",
            _ => $"{leftLabel}-{label}-{rightLabel}"
        };
        return relationship;
    }
}