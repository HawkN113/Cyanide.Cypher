using System.Text;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class LimitClause(StringBuilder limitClauses)
{
    private readonly List<string> _patterns = [];
    
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

    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (limitClauses.Length > 0)
        {
            limitClauses.Append(' ');
        }

        limitClauses.Append("LIMIT ");
        limitClauses.Append(string.Join("", _patterns));
    }
}