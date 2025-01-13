using System.Text;
using Cyanide.Cypher.Builders.Abstraction.Common;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class RemoveClause(StringBuilder removeClauses) : IFieldProperty<RemoveClause>
{
    private readonly List<string> _patterns = [];

    /// <summary>
    /// Remove a property using the REMOVE clause <br/>
    /// Sample: p.bornIn
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldAlias"></param>
    /// <returns></returns>
    public RemoveClause WithField(string fieldName, string fieldAlias)
    {
        _patterns.Add($"{fieldAlias}.{fieldName}");
        return this;
    }

    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (removeClauses.Length > 0)
        {
            removeClauses.Append(' ');
        }

        removeClauses.Append("REMOVE ");
        removeClauses.Append(string.Join("", _patterns));
    }
}