using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Helper;

namespace Cyanide.Cypher.Builders;

public sealed class CreateBuilder(CypherQueryBuilder parent, StringBuilder createClauses): IRelationship<CreateBuilder>, INode<CreateBuilder>
{
    private readonly List<string> _patterns = [];
    
    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <returns></returns>
    public CreateBuilder EmptyNode()
    {
        _patterns.AddRange(NodeHelper.EmptyNode());
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public CreateBuilder Node(Entity entity, Property[] properties = null)
    {
        _patterns.AddRange(NodeHelper.Node(entity, properties));
        return this;
    }
    
    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    public CreateBuilder Node(Entity entity, Property property)
    {
        _patterns.AddRange(NodeHelper.Node(entity, property));
        return this;
    }

    /// <summary>
    /// Add a relationship to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="relation">NonDirect (non-directed), Direct (directed), InDirect (in-directed), UnDirect (undirected), BiDirect (bidirectional) </param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public CreateBuilder Relationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "")
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
    public CreateBuilder Relationship(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null)
    {
        _patterns.Add(RelationshipHelper.Create(entity, relation, left, right));
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    public CypherQueryBuilder End()
    {
        if (_patterns.Count <= 0) return parent;
        if (createClauses.Length > 0)
        {
            createClauses.Append(", ");
        }

        createClauses.Append("CREATE ");
        createClauses.Append(string.Join(", ", _patterns));
        return parent;
    }
}