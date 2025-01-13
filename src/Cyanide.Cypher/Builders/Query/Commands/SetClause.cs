using System.Text;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class SetClause(StringBuilder setClauses)
{
    private readonly List<string> _patterns = [];
    private int _countProperties;

    /// <summary>
    /// Add a property for the SET clause
    /// </summary>
    /// <returns></returns>
    public SetClause WithField(string fieldName, string fieldAlias, string? value = null)
    {
        var labelProperty = $"{fieldAlias}.{fieldName}";
        _patterns.Add(value != null ? $"{labelProperty} = {value}" : labelProperty);
        _countProperties += 1;
        return this;
    }
    
    /// <summary>
    /// Add a string query for the SET clause
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public SetClause WithQuery(string condition)
    {
        _patterns.Add($"{condition}");
        return this;
    }

    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (setClauses.Length > 0)
        {
            setClauses.Append(' ');
        }

        setClauses.Append("SET ");
        setClauses.Append(_countProperties > 1 ? string.Join(", ", _patterns) : string.Join("", _patterns));
    }
}