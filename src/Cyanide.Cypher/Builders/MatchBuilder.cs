using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Helper;

namespace Cyanide.Cypher.Builders;

public sealed class MatchBuilder(CypherQueryBuilder parent, StringBuilder matchClauses): IRelationship<MatchBuilder>, INode<MatchBuilder>
{
    private readonly List<string> _patterns = [];

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <returns></returns>
    public MatchBuilder EmptyNode()
    {
        _patterns.AddRange(NodeHelper.EmptyNode());
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="alias"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public MatchBuilder Node(string type, string alias = "", Property[] properties = null)
    {
        _patterns.AddRange(NodeHelper.Node(type, alias, properties));
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    public MatchBuilder Node(string type, Property property)
    {
        _patterns.AddRange(NodeHelper.Node(type, property));
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
        _patterns.Add(RelationshipHelper.Create(type, relation, alias));
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    public CypherQueryBuilder End()
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