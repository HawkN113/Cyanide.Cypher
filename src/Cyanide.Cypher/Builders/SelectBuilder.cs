using System.Text;

namespace Cyanide.Cypher.Builders;

public class SelectBuilder(CypherQueryBuilder parent, StringBuilder returnClauses)
{
    private readonly List<string> _patterns = [];

    /// <summary>
    /// Return a node (entity) to the RETURN clause
    /// Sample: p
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public SelectBuilder Node(string type)
    {
        _patterns.Add($"{type}");
        return this;
    }
    
    /// <summary>
    /// Return a relationship to the RETURN clause
    /// Sample: type(r)
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public SelectBuilder Relation(string alias)
    {
        _patterns.Add($"type({alias})");
        return this;
    }

    /// <summary>
    /// Return a property to the RETURN clause
    /// Sample: p.bornIn
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public SelectBuilder Property(string propertyName, string alias)
    {
        _patterns.Add($"{alias}.{propertyName}");
        return this;
    }
    
    /// <summary>
    /// Return a property to the RETURN clause
    /// Sample: p
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public SelectBuilder Property(string alias)
    {
        _patterns.Add($"{alias}");
        return this;
    }

    /// <summary>
    /// Return a named property to the RETURN clause
    /// Sample: p.bornIn AS Born
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="alias"></param>
    /// <param name="aliasName"></param>
    /// <returns></returns>
    public SelectBuilder Property(string propertyName, string alias, string aliasName)
    {
        _patterns.Add($"{alias}.{propertyName} AS {aliasName}");
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    public CypherQueryBuilder End()
    {
        if (_patterns.Count <= 0) return parent;
        if (returnClauses.Length > 0)
        {
            returnClauses.Append(',');
        }

        returnClauses.Append("RETURN ");
        returnClauses.Append(string.Join(", ", _patterns));
        return parent;
    }
}