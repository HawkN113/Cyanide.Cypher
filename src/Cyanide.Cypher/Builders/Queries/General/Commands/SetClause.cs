using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.General.Commands;

public sealed class SetClause : 
    IBuilderInitializer
{
    private readonly List<string> _patterns = [];
    private int _countProperties;
    private StringBuilder _setClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _setClauses = clauseBuilder;
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

    public void End()
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