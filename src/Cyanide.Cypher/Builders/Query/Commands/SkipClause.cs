using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class SkipClause : 
    IBuilderInitializer
{
    private readonly List<string> _patterns = [];
    private StringBuilder _skipClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _skipClauses = clauseBuilder;
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

    public void End()
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