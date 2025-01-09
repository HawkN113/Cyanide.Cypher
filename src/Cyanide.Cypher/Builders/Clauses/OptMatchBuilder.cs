using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public sealed class OptMatchBuilder(CypherQueryBuilder parent, StringBuilder optMatchClauses): 
    IRelationship<OptMatchBuilder>, 
    INode<OptMatchBuilder>
{
    private readonly List<string> _patterns = [];
    
    /// <summary>
    /// Add a node (entity) to the OPTIONAL MATCH clause
    /// </summary>
    /// <returns></returns>
    public OptMatchBuilder EmptyNode()
    {
        _patterns.AddRange(NodeHelper.EmptyNode());
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public OptMatchBuilder Node(Entity entity)
    {
        _patterns.AddRange(NodeHelper.Node(entity));
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
    /// Add a relationship to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="relation"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public OptMatchBuilder Relationship(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        _patterns.Add(RelationshipHelper.Create(entity, relation, left, right));
        return this;
    }

    /// <summary>
    /// End the OPTIONAL MATCH clause
    /// </summary>
    /// <returns></returns>
    internal CypherQueryBuilder End()
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