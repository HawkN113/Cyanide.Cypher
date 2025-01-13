using System.Text;
using Cyanide.Cypher.Builders.Admin.Commands;

namespace Cyanide.Cypher.Builders.Admin;

internal sealed class CypherAdminBuilder: IAdminQuery
{
    private readonly StringBuilder _createAdmClauses = new();

    /// <summary>
    /// CREATE clause for admin management <br/>
    ///     - DATABASE  <br/>
    /// Sample: CREATE DATABASE db
    /// </summary>
    /// <param name="configureAdmCreate"></param>
    /// <returns></returns>
    public ICreateAdmQuery Create(Action<CreateAdmQuery> configureAdmCreate)
    {
        var createBuilder = new CreateAdmQuery(_createAdmClauses);
        configureAdmCreate(createBuilder);
        createBuilder.End();
        return this;
    }

    /// <summary>
    /// Generate Cypher query
    /// </summary>
    /// <returns>string</returns>
    public string Build()
    {
        StringBuilder queryBuilder = new();
        var clauses = new List<StringBuilder>
        {
            _createAdmClauses
        };
        foreach (var clause in clauses)
        {
            AppendClause(clause, queryBuilder);
        }

        return queryBuilder.ToString().Trim();
    }

    private static void AppendClause(StringBuilder clause, StringBuilder queryBuilder)
    {
        if (clause.Length <= 0) return;
        if (queryBuilder.Length > 0)
        {
            queryBuilder.Append(' ');
        }

        queryBuilder.Append(clause);
    }
}