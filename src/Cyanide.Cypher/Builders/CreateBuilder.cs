using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Helper;

namespace Cyanide.Cypher.Builders;

public sealed class CreateBuilder(CypherQueryBuilder parent, StringBuilder createClauses): IRelationship<CreateBuilder>
{
    private readonly List<string> _patterns = [];
    
    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <returns></returns>
    public CreateBuilder EmptyNode()
    {
        _patterns.Add("()");
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="alias"></param>
    /// <param name="property"></param>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    public CreateBuilder Node(string type, string alias = "", string property = null, string propertyValue = null)
    {
        if (!string.IsNullOrWhiteSpace(property) && !string.IsNullOrWhiteSpace(propertyValue))
        {
            _patterns.Add(!string.IsNullOrWhiteSpace(alias)
                ? $"({alias}:{type} {{{property}: {propertyValue}}})"
                : $"({type} {{{property}: {propertyValue}}})");
        }
        else
        {
            _patterns.Add(!string.IsNullOrWhiteSpace(alias)
                ? $"({alias}:{type})"
                : $"({type})");
        }
        return this;
    }
    
    /// <summary>
    /// Add a node (entity) to the MATCH clause
    /// </summary>
    /// <param name="type"></param>
    /// <param name="property"></param>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    public CreateBuilder Node(string type, string property, string propertyValue)
    {
        _patterns.Add($"(:{type} {{{property}: {propertyValue}}})");
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
        createClauses.Append(string.Join("", _patterns));
        return parent;
    }
}