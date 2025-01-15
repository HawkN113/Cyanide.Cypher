using System.Text;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class SkipClause
{
    private readonly List<string> _patterns = [];
    private readonly StringBuilder _skipClauses;

    public SkipClause(StringBuilder skipClauses)
    {
        _skipClauses= skipClauses;
    }
    
    public SkipClause()
    {
        _skipClauses = new StringBuilder();
    }
    
    /// <summary>
    /// Add a string number of returned rows for the SKIP clause <br/>
    /// Sample: LIMIT 1 + toInteger(3 * rand())
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public SkipClause WithCount(string condition)
    {
        _patterns.Add($"{condition}");
        return this;
    }
    
    /// <summary>
    /// Add a positive number of returned rows for the SKIP clause <br/>
    /// Sample: LIMIT 3
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public SkipClause WithCount(int number)
    {
        _patterns.Add($"{number}");
        return this;
    }

    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (_skipClauses.Length > 0)
        {
            _skipClauses.Append(' ');
        }

        _skipClauses.Append("SKIP ");
        _skipClauses.Append(string.Join("", _patterns));
    }
}