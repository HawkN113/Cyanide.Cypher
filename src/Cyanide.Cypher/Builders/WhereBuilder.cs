using System.Text;

namespace Cyanide.Cypher.Builders;

public sealed class WhereBuilder(CypherQueryBuilder parent, StringBuilder whereClauses)
{
    private readonly List<string> _patterns = [];
    public WhereBuilder Query(string query)
    {
        _patterns.Add($"{query}");
        return this;
    }
    
    public WhereBuilder Or()
    {
        _patterns.Add(" OR ");
        return this;
    }
    public WhereBuilder Not()
    {
        _patterns.Add(" NOT ");
        return this;
    }
    public WhereBuilder Xor()
    {
        _patterns.Add(" XOR ");
        return this;
    }
    public WhereBuilder IsNotNull()
    {
        _patterns.Add(" IS NOT NULL ");
        return this;
    }
    
    public WhereBuilder And()
    {
        _patterns.Add(" AND ");
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    public CypherQueryBuilder EndWhere()
    {
        if (_patterns.Count <= 0) return parent;
        if (whereClauses.Length > 0)
        {
            whereClauses.Append(' ');
        }
        whereClauses.Append("WHERE ");
        whereClauses.Append(string.Join("", _patterns));
        return parent;
    }
}