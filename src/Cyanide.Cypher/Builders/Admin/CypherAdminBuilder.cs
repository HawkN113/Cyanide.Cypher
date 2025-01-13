using System.Text;
using Cyanide.Cypher.Builders.Admin;
using Cyanide.Cypher.Builders.Admin.Commands;

namespace Cyanide.Cypher.Builders;

internal sealed class CypherAdminBuilder: IAdminQuery
{
    private readonly StringBuilder _createDbClauses = new();
    private readonly StringBuilder _createUserClauses = new();

    /// <summary>
    /// CREATE DATABASE clause for admin management <br/>
    /// CREATE OR REPLACE DATABASE clause for admin management <br/>
    /// Sample: CREATE DATABASE db
    /// For Enterprise Edition
    /// </summary>
    /// <param name="configureDbCreate"></param>
    /// <returns></returns>
    public ICreateDbQuery Create(Action<CreateDbQuery> configureDbCreate)
    {
        var createBuilder = new CreateDbQuery(_createDbClauses);
        configureDbCreate(createBuilder);
        createBuilder.End();
        return this;
    }

    /// <summary>
    /// CREATE USER clause for admin management <br/>
    /// CREATE OR USER USER clause for admin management <br/>
    /// Sample: CREATE OR REPLACE USER name <br/>
    ///         <tab/> SET PLAINTEXT PASSWORD 'password' <br/>
    /// </summary>
    /// <param name="configureUserCreate"></param>
    /// <returns></returns>
    public ICreateUserQuery Create(Action<CreateUserQuery> configureUserCreate)
    {
        var createBuilder = new CreateUserQuery(_createUserClauses);
        configureUserCreate(createBuilder);
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
            _createDbClauses,
            _createUserClauses
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