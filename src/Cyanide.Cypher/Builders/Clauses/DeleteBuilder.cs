﻿using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public sealed class DeleteBuilder(CypherQueryBuilder parent, StringBuilder createClauses, bool isDetachDelete): 
    IRelationship<DeleteBuilder>, 
    INode<DeleteBuilder>
{
    private readonly List<string> _patterns = [];
    
    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <returns></returns>
    public DeleteBuilder EmptyNode()
    {
        _patterns.AddRange(NodeHelper.EmptyNode());
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the DELETE clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public DeleteBuilder Node(Entity entity)
    {
        _patterns.AddRange(NodeHelper.Node(entity));
        return this;
    }
    
    /// <summary>
    /// Return a node (entity) to the DELETE clause
    /// Sample: p
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public DeleteBuilder Node(string type)
    {
        _patterns.Add($"{type}");
        return this;
    }

    /// <summary>
    /// Add a relationship to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">NonDirect (non-directed), Direct (directed), InDirect (in-directed), UnDirect (undirected), BiDirect (bidirectional) </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public DeleteBuilder Relationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
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
    public DeleteBuilder Relationship(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        _patterns.Add(RelationshipHelper.Create(entity, relation, left, right));
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    internal CypherQueryBuilder End()
    {
        if (_patterns.Count <= 0) return parent;
        if (createClauses.Length > 0)
        {
            createClauses.Append(", ");
        }

        createClauses.Append(!isDetachDelete ? "DELETE " : "DETACH DELETE ");
        createClauses.Append(string.Join("", _patterns));
        return parent;
    }
}