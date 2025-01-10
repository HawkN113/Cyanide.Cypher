using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public sealed class MatchBuilder(StringBuilder matchClauses): IRelationship<MatchBuilder>, INode<MatchBuilder>, IEmptyNode<MatchBuilder>
{
    private readonly List<string> _patterns = [];

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <returns></returns>
    public MatchBuilder WithEmptyNode()
    {
        _patterns.AddRange(NodeHelper.EmptyNode());
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MatchBuilder WithNode(Entity entity)
    {
        _patterns.AddRange(NodeHelper.Node(entity));
        return this;
    }

    /// <summary>
    /// Add a relationship to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">NonDirect (non-directed), Direct (directed), InDirect (in-directed), UnDirect (undirected), BiDirect (bidirectional) </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public MatchBuilder WithRelationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
    {
        _patterns.Add(RelationshipHelper.Create(type, relation, alias));
        return this;
    }
    
    /// <summary>
    /// Add a relationship to the MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="relation"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public MatchBuilder WithRelationship(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        _patterns.Add(RelationshipHelper.Create(entity, relation, left, right));
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (matchClauses.Length > 0)
        {
            matchClauses.Append(' ');
        }

        matchClauses.Append("MATCH ");
        matchClauses.Append(string.Join("", _patterns));
    }
}