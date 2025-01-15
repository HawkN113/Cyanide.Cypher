﻿using System.Text;
using Cyanide.Cypher.Builders.Abstraction.Common;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class DeleteClause: IRelationship<DeleteClause>, INode<DeleteClause>, IEmptyNode<DeleteClause>
{
    private readonly List<string> _patterns = [];
    private readonly StringBuilder _createClauses;

    public DeleteClause(StringBuilder createClauses)
    {
        _createClauses = createClauses;
    }
    
    public DeleteClause()
    {
        _createClauses = new StringBuilder();
    }
    
    /// <summary>
    /// Delete a node (entity) to the DELETE clause
    /// </summary>
    /// <returns></returns>
    public DeleteClause WithEmptyNode()
    {
        _patterns.AddRange(NodePatternBuilder.CreateEmptyNode());
        return this;
    }

    /// <summary>
    /// Delete a node (entity) to the DELETE clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public DeleteClause WithNode(Entity entity)
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
    public DeleteClause WithRelationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
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
    public DeleteClause WithRelationship(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        _patterns.Add(RelationshipPatternHelper.Create(entity, relation, left, right));
        return this;
    }
    
    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (_createClauses.Length > 0)
        {
            _createClauses.Append(' ');
        }

        _createClauses.Append("DELETE ");
        _createClauses.Append(string.Join("", _patterns));
    }
}