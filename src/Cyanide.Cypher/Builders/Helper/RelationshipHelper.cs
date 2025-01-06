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
}