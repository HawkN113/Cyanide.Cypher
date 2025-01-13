using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Query;

public sealed class WithClause(StringBuilder withClauses): IFieldProperty<WithClause>, IFieldAlias<WithClause>, IFieldType<WithClause>, IFunctionCount<WithClause>, IFunctionToUpper<WithClause>
{
    private readonly List<string> _patterns = [];
    
    public WithClause Count(string fieldName, string fieldAlias, string aliasName)
    {
        _patterns.Add(FunctionsPatternBuilder.Count(fieldName, fieldAlias, aliasName));
        return this;
    }

    public WithClause ToUpper(string fieldName, string fieldAlias, string aliasName)
    {
        _patterns.Add(FunctionsPatternBuilder.ToUpper(fieldName, fieldAlias, aliasName));
        return this;
    }

    /// <summary>
    /// Return a type for the WITH clause <br/>
    /// Sample: type(r)
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public WithClause WithType(string alias)
    {
        _patterns.Add($"type({alias})");
        return this;
    }

    /// <summary>
    /// Return a type with alias name for the WITH clause <br/>
    /// Sample: type(r) AS t
    /// </summary>
    /// <param name="alias"></param>
    /// <param name="aliasName"></param>
    /// <returns></returns>
    public WithClause WithType(string alias, string aliasName)
    {
        _patterns.Add($"type({alias}) AS {aliasName}");
        return this;
    }
    
    /// <summary>
    /// Return a named property for the WITH clause <br/>
    /// Sample: p.bornIn AS Born
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldAlias"></param>
    /// <param name="aliasName"></param>
    /// <returns></returns>
    public WithClause WithField(string fieldName, string fieldAlias, string aliasName)
    {
        _patterns.Add($"{fieldAlias}.{fieldName} AS {aliasName}");
        return this;
    }

    /// <summary>
    /// Return a property for the WITH clause <br/>
    /// Sample: p.bornIn
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldAlias"></param>
    /// <returns></returns>
    public WithClause WithField(string fieldName, string fieldAlias)
    {
        _patterns.Add($"{fieldAlias}.{fieldName}");
        return this;
    }

    /// <summary>
    /// Return a node (entity) for the WITH clause <br/>
    /// Sample: p
    /// </summary>
    /// <param name="fieldAlias"></param>
    /// <returns></returns>
    public WithClause WithField(string fieldAlias)
    {
        _patterns.Add($"{fieldAlias}");
        return this;
    }
    
    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (withClauses.Length > 0)
        {
            withClauses.Append(' ');
        }

        withClauses.Append("WITH ");
        withClauses.Append(string.Join(", ", _patterns));
    }
}