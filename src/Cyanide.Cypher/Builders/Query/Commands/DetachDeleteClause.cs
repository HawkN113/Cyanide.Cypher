using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Abstraction.Nodes;
using Cyanide.Cypher.Builders.Abstraction.Relations;
using Cyanide.Cypher.Builders.Helper;
using Cyanide.Cypher.Builders.Models;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class DetachDeleteClause : 
    IBuilderInitializer, 
    IRelation<DetachDeleteClause>, 
    INode<DetachDeleteClause>, 
    IEmptyNode<DetachDeleteClause>
{
    private readonly List<string> _patterns = [];
    private StringBuilder _detachDeleteClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _detachDeleteClauses = clauseBuilder;
    }
    
    /// <summary>
    /// Delete a node (entity) to the DELETE clause
    /// </summary>
    /// <returns></returns>
    public DetachDeleteClause WithEmptyNode()
    {
        _patterns.AddRange(NodePatternBuilder.CreateEmptyNode());
        return this;
    }

    /// <summary>
    /// Delete a node (entity) to the DELETE clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public DetachDeleteClause WithNode(Entity entity)
    {
        _patterns.AddRange(NodePatternBuilder.CreateNode(entity));
        return this;
    }

    /// <summary>
    /// Delete a node (entity) to the DELETE clause <br/>
    /// Sample: p
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public void WithNode(string type)
    {
        _patterns.Add($"{type}");
    }

    /// <summary>
    /// Add a relationship to the DELETE clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">NonDirect (non-directed), Direct (directed), InDirect (in-directed), UnDirect (undirected), BiDirect (bidirectional) </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public DetachDeleteClause WithRelation(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
    {
        _patterns.Add(RelationshipPatternHelper.Create(type, relation, alias));
        return this;
    }

    /// <summary>
    /// Add a relationship to the DELETE clause
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="relation"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public DetachDeleteClause WithRelation(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        _patterns.Add(RelationshipPatternHelper.Create(entity, relation, left, right));
        return this;
    }
    
    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_detachDeleteClauses.Length > 0)
        {
            _detachDeleteClauses.Append(' ');
        }

        _detachDeleteClauses.Append("DETACH DELETE ");
        _detachDeleteClauses.Append(string.Join("", _patterns));
    }
}