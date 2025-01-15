using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Query.Commands;

public sealed class WhereSubClause(StringBuilder whereClauses) : IClause
{
    private readonly List<string> _patterns = [];


    /// <summary>
    /// Add a query for the property <br/>
    /// Sample: p >= 1 <br/>
    /// Sample: p.name = 'Neo'
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public WhereSubClause Query(string query)
    {
        _patterns.Add($"{query}");
        return this;
    }

    /// <summary>
    /// Boolean operator OR in the WHERE clause
    /// </summary>
    /// <param name="nestedConditions"></param>
    public void Or(Action<WhereSubClause> nestedConditions)
    {
        _patterns.Add(" OR ");
        nestedConditions(this);
    }

    /// <summary>
    /// Boolean operator NOT in the WHERE clause
    /// </summary>
    /// <param name="nestedConditions"></param>
    public void Not(Action<WhereSubClause> nestedConditions)
    {
        _patterns.Add(" NOT ");
        nestedConditions(this);
    }

    /// <summary>
    /// Boolean operator XOR in the WHERE clause
    /// </summary>
    /// <param name="nestedConditions"></param>
    public void Xor(Action<WhereSubClause> nestedConditions)
    {
        _patterns.Add(" XOR ");
        nestedConditions(this);
    }

    /// <summary>
    /// Comparison operator IS NOT NULL in the WHERE clause
    /// </summary>
    /// <param name="query"></param>
    public void IsNotNull(string query)
    {
        _patterns.Add($"{query} IS NOT NULL");
    }
    
    /// <summary>
    /// Comparison operator IS NULL in the WHERE clause
    /// </summary>
    /// <param name="query"></param>
    public void IsNull(string query)
    {
        _patterns.Add($"{query} IS NULL");
    }

    /// <summary>
    /// Boolean operator AND in the WHERE clause
    /// </summary>
    /// <param name="nestedConditions"></param>
    public void And(Action<WhereSubClause> nestedConditions)
    {
        _patterns.Add(" AND ");
        nestedConditions(this);
    }
    
    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (whereClauses.Length > 0)
        {
            whereClauses.Append(' ');
        }

        whereClauses.Append("WHERE ");
        whereClauses.Append(string.Join("", _patterns));
    }
}