using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class LimitClause : 
    IBuilderInitializer
{
    private readonly List<string> _patterns = [];
    private StringBuilder _limitClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _limitClauses = clauseBuilder;
    }

    /// <summary>
    /// Add a string number of returned rows for the LIMIT clause <br/>
    /// Sample: LIMIT 1 + toInteger(3 * rand())
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public LimitClause WithCount(string condition)
    {
        _patterns.Add($"{condition}");
        return this;
    }
    
    /// <summary>
    /// Add a positive number of returned rows for the LIMIT clause <br/>
    /// Sample: LIMIT 3
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public LimitClause WithCount(int number)
    {
        _patterns.Add($"{number}");
        return this;
    }

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_limitClauses.Length > 0)
        {
            _limitClauses.Append(' ');
        }

        _limitClauses.Append("LIMIT ");
        _limitClauses.Append(string.Join("", _patterns));
    }
}