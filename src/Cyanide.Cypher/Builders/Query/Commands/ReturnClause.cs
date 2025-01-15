using System.Text;
using Cyanide.Cypher.Builders.Abstraction.Common;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class ReturnClause: IFieldProperty<ReturnClause>, IFieldAlias<ReturnClause>, IFieldType<ReturnClause>
{
    private readonly List<string> _patterns = [];
    private readonly StringBuilder _returnClauses;

    public ReturnClause(StringBuilder returnClauses)
    {
        _returnClauses = returnClauses;
    }
    
    public ReturnClause()
    {
        _returnClauses = new StringBuilder();
    }
    
    /// <summary>
    /// Return a type to the RETURN clause <br/>
    /// Sample: type(r)
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public ReturnClause WithType(string alias)
    {
        _patterns.Add($"type({alias})");
        return this;
    }

    /// <summary>
    /// Return a type with alias name  to the RETURN clause <br/>
    /// Sample: type(r) AS t
    /// </summary>
    /// <param name="alias"></param>
    /// <param name="aliasName"></param>
    /// <returns></returns>
    public ReturnClause WithType(string alias, string aliasName)
    {
        _patterns.Add($"type({alias}) AS {aliasName}");
        return this;
    }

    /// <summary>
    /// Return a named property to the RETURN clause <br/>
    /// Sample: p.bornIn AS Born
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldAlias"></param>
    /// <param name="aliasName"></param>
    /// <returns></returns>
    public ReturnClause WithField(string fieldName, string fieldAlias, string aliasName)
    {
        _patterns.Add($"{fieldAlias}.{fieldName} AS {aliasName}");
        return this;
    }

    /// <summary>
    /// Return a property to the RETURN clause <br/>
    /// Sample: p.bornIn
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldAlias"></param>
    /// <returns></returns>
    public ReturnClause WithField(string fieldName, string fieldAlias)
    {
        _patterns.Add($"{fieldAlias}.{fieldName}");
        return this;
    }

    /// <summary>
    /// Return a node (entity) to the RETURN clause <br/>
    /// Sample: p
    /// </summary>
    /// <param name="fieldAlias"></param>
    /// <returns></returns>
    public ReturnClause WithField(string fieldAlias)
    {
        _patterns.Add($"{fieldAlias}");
        return this;
    }
    
    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (_returnClauses.Length > 0)
        {
            _returnClauses.Append(' ');
        }

        _returnClauses.Append("RETURN ");
        _returnClauses.Append(string.Join(", ", _patterns));
    }

}