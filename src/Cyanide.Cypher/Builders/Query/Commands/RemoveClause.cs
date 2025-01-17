using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Abstraction.Fields;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class RemoveClause: 
    IBuilderInitializer, 
    IFieldProperty<RemoveClause>
{
    private readonly List<string> _patterns = [];
    private StringBuilder _removeClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _removeClauses = clauseBuilder;
    }

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

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_removeClauses.Length > 0)
        {
            _removeClauses.Append(' ');
        }

        _removeClauses.Append("REMOVE ");
        _removeClauses.Append(string.Join("", _patterns));
    }
}