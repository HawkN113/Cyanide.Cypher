using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Helper;

namespace Cyanide.Cypher.Builders;

public sealed class OptMatchBuilder(CypherQueryBuilder parent, StringBuilder optMatchClauses): IRelationship<OptMatchBuilder>
{
    private readonly List<string> _patterns = [];

    /// <summary>
    /// Add a node (entity) to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public OptMatchBuilder Node(string type, string alias = "")
    {
        _patterns.Add(!string.IsNullOrWhiteSpace(alias) ? $"({alias}:{type})" : $"({type})");
        return this;
    }
    
    /// <summary>
    /// Add a node (entity) to the OPTIONAL MATCH clause
    /// </summary>
    /// <returns></returns>
    public OptMatchBuilder EmptyNode()
    {
        _patterns.Add("()");
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="alias"></param>
    /// <param name="property"></param>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    public OptMatchBuilder Node(string type, string alias, string property, string propertyValue)
    {
        _patterns.Add($"({alias}:{type} {{{property}: {propertyValue}}})");
        return this;
    }
    
    /// <summary>
    /// Add a node (entity) to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="property"></param>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    public OptMatchBuilder Node(string type, string property, string propertyValue)
    {
        _patterns.Add($"(:{type} {{{property}: {propertyValue}}})");
        return this;
    }

    /// <summary>
    /// Add a relationship to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">NonDirect (non-directed), Direct (directed), InDirect (in-directed), UnDirect (undirected), BiDirect (bidirectional) </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public OptMatchBuilder Relationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
    {
        _patterns.Add(RelationshipHelper.Create(type, relation, alias));
        return this;
    }

    /// <summary>
    /// End the OPTIONAL MATCH clause
    /// </summary>
    /// <returns></returns>
    public CypherQueryBuilder End()
    {
        if (_patterns.Count <= 0) return parent;
        if (optMatchClauses.Length > 0)
        {
            optMatchClauses.Append(' ');
        }

        optMatchClauses.Append("OPTIONAL MATCH ");
        optMatchClauses.Append(string.Join("", _patterns));
        return parent;
    }
}