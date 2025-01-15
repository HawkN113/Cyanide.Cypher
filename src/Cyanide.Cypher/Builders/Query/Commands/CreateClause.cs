using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Abstraction.Common;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class CreateClause: IClause, IRelationship<CreateClause>, INode<CreateClause>
{
    private readonly List<string> _patterns = [];
    private int _countNodes;
    private readonly StringBuilder _createClauses;

    public CreateClause(StringBuilder createClauses)
    {
        _createClauses = createClauses;
    }
    
    public CreateClause()
    {
        _createClauses = new StringBuilder();
    }

    /// <summary>
    /// Add a node (entity) to the CREATE clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public CreateClause WithNode(Entity entity)
    {
        _patterns.AddRange(NodePatternBuilder.CreateNode(entity));
        _countNodes += 1;
        return this;
    }

    /// <summary>
    /// Add a relationship to the CREATE clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">NonDirect (non-directed), Direct (directed), InDirect (in-directed), UnDirect (undirected), BiDirect (bidirectional) </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public CreateClause WithRelationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
    {
        _patterns.Add(RelationshipPatternHelper.Create(type, relation, alias));
        return this;
    }

    /// <summary>
    /// Add a relationship to the CREATE clause
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="relation"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public CreateClause WithRelationship(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
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

        _createClauses.Append("CREATE ");
        _createClauses.Append(_countNodes > 1 ? string.Join(", ", _patterns) : string.Join("", _patterns));
    }
}