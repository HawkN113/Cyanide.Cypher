using System.Text;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class SetClause
{
    private readonly List<string> _patterns = [];
    private int _countProperties;
    private readonly StringBuilder _setClauses;

    public SetClause(StringBuilder setClauses)
    {
        _setClauses = setClauses;
    }
    
    public SetClause()
    {
        _setClauses = new StringBuilder();
    }

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
        if (_setClauses.Length > 0)
        {
            _setClauses.Append(' ');
        }

        _setClauses.Append("SET ");
        _setClauses.Append(_countProperties > 1 ? string.Join(", ", _patterns) : string.Join("", _patterns));
    }
}