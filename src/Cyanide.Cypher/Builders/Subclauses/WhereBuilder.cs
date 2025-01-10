using System.Text;

namespace Cyanide.Cypher.Builders;

public sealed class WhereBuilder(StringBuilder whereClauses)
{
    private readonly List<string> _patterns = [];

    public WhereBuilder Query(string query)
    {
        _patterns.Add($"{query}");
        return this;
    }

    public void Or(Action<WhereBuilder> nestedConditions)
    {
        _patterns.Add(" OR ");
        nestedConditions(this);
    }

    public void Not(Action<WhereBuilder> nestedConditions)
    {
        _patterns.Add(" NOT ");
        nestedConditions(this);
    }

    public void Xor(Action<WhereBuilder> nestedConditions)
    {
        _patterns.Add(" XOR ");
        nestedConditions(this);
    }

    public void IsNotNull(string query)
    {
        _patterns.Add($"{query} IS NOT NULL");
    }
    
    public void IsNull(string query)
    {
        _patterns.Add($"{query} IS NULL");
    }

    public void And(Action<WhereBuilder> nestedConditions)
    {
        _patterns.Add(" AND ");
        nestedConditions(this);
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    internal void End()
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