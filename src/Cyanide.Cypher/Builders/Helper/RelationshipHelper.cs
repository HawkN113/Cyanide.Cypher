using System.Text;

namespace Cyanide.Cypher.Builders;

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

    public static string Create(Entity entity, RelationshipType relation = RelationshipType.NonDirect,
        Entity? left = null,
        Entity? right = null)
    {
        var label = !string.IsNullOrWhiteSpace(entity.Alias) ? $"[{entity.Alias}:{entity.Type}]" : $"[:{entity.Type}]";

        var leftLabel = left is not null
            ? new StringBuilder().Append(string.Join("", NodeHelper.Node(left))).ToString()
            : string.Empty;
        var rightLabel = right is not null
            ? new StringBuilder().Append(string.Join("", NodeHelper.Node(right))).ToString()
            : string.Empty;

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