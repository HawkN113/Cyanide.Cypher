using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public class SelectBuilder(StringBuilder returnClauses): IField<SelectBuilder>
{
    private readonly List<string> _patterns = [];
    
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
    /// Return a named property to the RETURN clause
    /// Sample: p.bornIn AS Born
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldAlias"></param>
    /// <param name="aliasName"></param>
    /// <returns></returns>
    public SelectBuilder WithField(string fieldName, string fieldAlias, string aliasName)
    {
        _patterns.Add($"{fieldAlias}.{fieldName} AS {aliasName}");
        return this;
    }

    /// <summary>
    /// Return a property to the RETURN clause
    /// Sample: p.bornIn
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldAlias"></param>
    /// <returns></returns>
    public SelectBuilder WithField(string fieldName, string fieldAlias)
    {
        _patterns.Add($"{fieldAlias}.{fieldName}");
        return this;
    }

    /// <summary>
    /// Return a node (entity) to the RETURN clause
    /// Sample: p
    /// </summary>
    /// <param name="fieldAlias"></param>
    /// <returns></returns>
    public SelectBuilder WithField(string fieldAlias)
    {
        _patterns.Add($"{fieldAlias}");
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (returnClauses.Length > 0)
        {
            returnClauses.Append(',');
        }

        returnClauses.Append("RETURN ");
        returnClauses.Append(string.Join(", ", _patterns));
    }

}