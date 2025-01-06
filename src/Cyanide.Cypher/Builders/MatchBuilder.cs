using System.Text;

namespace Cyanide.Cypher.Builders;

public sealed class MatchBuilder(CypherQueryBuilder parent, StringBuilder matchClauses)
{
    private readonly List<string> _patterns = [];

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public MatchBuilder Node(string type, string alias = "")
    {
        _patterns.Add(!string.IsNullOrWhiteSpace(alias) ? $"({alias}:{type})" : $"({type})");
        return this;
    }

    /// <summary>
    /// Add a relationship to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">NonDirect (non-directed), Direct (directed), InDirect (in-directed), UnDirect (undirected), BiDirect (bidirectional) </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public MatchBuilder Relationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
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
        _patterns.Add(relationship);
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    public CypherQueryBuilder EndMatch()
    {
        if (_patterns.Count <= 0) return parent;
        if (matchClauses.Length > 0)
        {
            matchClauses.Append(' ');
        }
        matchClauses.Append("MATCH ");
        matchClauses.Append(string.Join("", _patterns));
        return parent;
    }
}