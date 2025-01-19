using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Abstraction.Nodes;
using Cyanide.Cypher.Builders.Abstraction.Relations;
using Cyanide.Cypher.Builders.Helper;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class MatchClause : 
    IBuilderInitializer, 
    IRelation<MatchClause>,
    INode<MatchClause>, 
    IEmptyNode<MatchClause>
{
    private readonly List<string> _patterns = [];
    private StringBuilder _matchClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _matchClauses = clauseBuilder;
    }
    
    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <returns></returns>
    public MatchClause WithEmptyNode()
    {
        _patterns.AddRange(NodePatternBuilder.CreateEmptyNode());
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MatchClause WithNode(Entity entity)
    {
        _patterns.AddRange(NodePatternBuilder.CreateNode(entity));
        return this;
    }

    /// <summary>
    /// Add a relationship to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">
    /// <param name="alias"></param>
    /// NonDirect (non-directed) <br/>
    /// Direct (directed) <br/>
    /// InDirect (in-directed) <br/>
    /// UnDirect (undirected) <br/>
    /// BiDirect (bidirectional)
    /// </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public MatchClause WithRelation(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
    {
        _patterns.Add(RelationshipPatternHelper.Create(type, relation, alias));
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
    public MatchClause WithRelation(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        _patterns.Add(RelationshipPatternHelper.Create(entity, relation, left, right));
        return this;
    }
    
    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_matchClauses.Length > 0)
        {
            _matchClauses.Append(' ');
        }

        _matchClauses.Append("MATCH ");
        _matchClauses.Append(string.Join("", _patterns));
    }
}