using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public sealed class CreateBuilder(StringBuilder createClauses): IRelationship<CreateBuilder>, INode<CreateBuilder>
{
    private readonly List<string> _patterns = [];
    private int _countNodes;

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public CreateBuilder WithNode(Entity entity)
    {
        _patterns.AddRange(NodeHelper.Node(entity));
        _countNodes += 1;
        return this;
    }

    /// <summary>
    /// Add a relationship to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">NonDirect (non-directed), Direct (directed), InDirect (in-directed), UnDirect (undirected), BiDirect (bidirectional) </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public CreateBuilder WithRelationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
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
    public CreateBuilder WithRelationship(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
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
        if (createClauses.Length > 0)
        {
            createClauses.Append(", ");
        }

        createClauses.Append("CREATE ");
        createClauses.Append(_countNodes > 1 ? string.Join(", ", _patterns) : string.Join("", _patterns));
    }
}