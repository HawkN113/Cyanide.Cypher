using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Abstraction.Nodes;
using Cyanide.Cypher.Builders.Abstraction.Relations;
using Cyanide.Cypher.Builders.Helper;

namespace Cyanide.Cypher.Builders.Queries.General.Commands;

public sealed class OptMatchClause : 
    IBuilderInitializer, 
    IRelation<OptMatchClause>, 
    INode<OptMatchClause>, 
    IEmptyNode<OptMatchClause>
{
    private readonly List<string> _patterns = [];
    private StringBuilder _optMatchClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _optMatchClauses = clauseBuilder;
    }

    /// <summary>
    /// Add a node (entity) to the OPTIONAL MATCH clause
    /// </summary>
    /// <returns></returns>
    public OptMatchClause WithEmptyNode()
    {
        _patterns.AddRange(NodePatternBuilder.CreateEmptyNode());
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public OptMatchClause WithNode(Entity entity)
    {
        _patterns.AddRange(NodePatternBuilder.CreateNode(entity));
        return this;
    }

    /// <summary>
    /// Add a relationship to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">
    /// NonDirect (non-directed) <br/>
    /// Direct (directed) <br/>
    /// InDirect (in-directed) <br/>
    /// UnDirect (undirected) <br/>
    /// BiDirect (bidirectional)
    /// </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public OptMatchClause WithRelation(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
    {
        _patterns.Add(RelationshipPatternHelper.Create(type, relation, alias));
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
    public OptMatchClause WithRelation(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        _patterns.Add(RelationshipPatternHelper.Create(entity, relation, left, right));
        return this;
    }

    /// <summary>
    /// Add a basic relationship to the OPTIONAL MATCH clause
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="relation"></param>
    /// <returns></returns>
    public OptMatchClause WithRelation(Entity left, Entity right, BasicRelationshipType relation = BasicRelationshipType.RelatedTo)
    {
        _patterns.Add(RelationshipPatternHelper.Create(left, right, relation));
        return this;
    }

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_optMatchClauses.Length > 0)
        {
            _optMatchClauses.Append(' ');
        }

        _optMatchClauses.Append("OPTIONAL MATCH ");
        _optMatchClauses.Append(string.Join("", _patterns));
    }
}