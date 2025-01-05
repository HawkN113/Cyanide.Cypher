using System.Text;

namespace Cyanide.Cypher.Builders;

public class CypherQueryBuilder
{
    private readonly StringBuilder _matchClauses = new();
    private readonly StringBuilder _whereClauses = new();
    private readonly StringBuilder _returnClauses = new();
    private readonly List<string> _parameters = new();

    // Add a MATCH clause
    public CypherQueryBuilder Match(string pattern)
    {
        if (_matchClauses.Length > 0)
        {
            _matchClauses.Append(" ");
        }
        _matchClauses.Append($"MATCH {pattern}");
        return this;
    }

    // Add a WHERE clause
    public CypherQueryBuilder Where(string condition)
    {
        if (_whereClauses.Length == 0)
        {
            _whereClauses.Append("WHERE ");
        }
        else
        {
            _whereClauses.Append(" AND ");
        }
        _whereClauses.Append(condition);
        return this;
    }

    // Add RETURN clause
    public CypherQueryBuilder Return(params string[] fields)
    {
        if (_returnClauses.Length == 0)
        {
            _returnClauses.Append("RETURN ");
        }
        else
        {
            _returnClauses.Append(", ");
        }
        _returnClauses.Append(string.Join(", ", fields));
        return this;
    }

    // Add a parameter (optional, for dynamic queries)
    public CypherQueryBuilder WithParameter(string key, string value)
    {
        _parameters.Add($"${key} = '{value}'");
        return this;
    }

    // Build the query
    public string Build()
    {
        StringBuilder queryBuilder = new();

        if (_matchClauses.Length > 0)
        {
            queryBuilder.Append(_matchClauses);
            queryBuilder.Append(" ");
        }

        if (_whereClauses.Length > 0)
        {
            queryBuilder.Append(_whereClauses);
            queryBuilder.Append(" ");
        }

        if (_returnClauses.Length > 0)
        {
            queryBuilder.Append(_returnClauses);
        }

        if (_parameters.Count > 0)
        {
            queryBuilder.AppendLine();
            queryBuilder.Append("Parameters: ");
            queryBuilder.Append(string.Join(", ", _parameters));
        }

        return queryBuilder.ToString().Trim();
    }
}