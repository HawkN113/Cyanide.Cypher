using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Admin.Commands;

namespace Cyanide.Cypher.Builders.Admin;

internal sealed class CypherAdminBuilder() : BaseQueryBuilder(DefaultClauses), IAdminQuery
{
    private static readonly Dictionary<string, StringBuilder> DefaultClauses =
        Enum.GetValues<AdminClauseKeys>().ToDictionary(key => key.ToString(), _ => new StringBuilder());
    
    /// <summary>
    /// CREATE DATABASE clause for admin management <br/>
    /// CREATE OR REPLACE DATABASE clause for admin management <br/>
    /// Sample: CREATE DATABASE db
    /// For Enterprise Edition
    /// </summary>
    /// <param name="configureDbCreate"></param>
    /// <returns></returns>
    public ICreateDbQuery Create(Action<CreateDbQuery> configureDbCreate) =>
        ConfigureQueryBuilder<ICreateDbQuery, CreateDbQuery>(AdminClauseKeys.CreateDb.ToString(), configureDbCreate);

    /// <summary>
    /// CREATE USER clause for admin management <br/>
    /// CREATE OR USER clause for admin management <br/>
    /// Sample: CREATE OR REPLACE USER name <br/>
    ///         <tab/> SET PLAINTEXT PASSWORD 'password' <br/>
    /// </summary>
    /// <param name="configureUserCreate"></param>
    /// <returns></returns>
    public ICreateUserQuery Create(Action<CreateUserQuery> configureUserCreate) =>
        ConfigureQueryBuilder<ICreateUserQuery, CreateUserQuery>(AdminClauseKeys.CreateUser.ToString(),
            configureUserCreate);

    /// <summary>
    /// SHOW DATABASE(S) clause for admin management <br/>
    /// Sample: SHOW DATABASE db YIELD *
    /// </summary>
    /// <param name="configureDbShow"></param>
    public IShowDbQuery Show(Action<ShowDbQuery> configureDbShow) => ConfigureQueryBuilder<IShowDbQuery, ShowDbQuery>(
        AdminClauseKeys.ShowDb.ToString(),
        configureDbShow);

    /// <summary>
    /// SHOW USER(S) clause for admin management <br/>
    /// Sample: SHOW CURRENT USER db YIELD *
    /// </summary>
    /// <param name="configureUserShow"></param>
    public IShowUserQuery Show(Action<ShowUserQuery> configureUserShow) =>
        ConfigureQueryBuilder<IShowUserQuery, ShowUserQuery>(AdminClauseKeys.ShowUser.ToString(), configureUserShow);

    /// <summary>
    /// Generates the final Cypher query by concatenating all configured clauses.
    /// </summary>
    /// <returns>The fully constructed Cypher query as a string.</returns>
    public string Build()
    {
        var finalQuery = new StringBuilder();
        foreach (var clause in allClauses.Values.Where(clause => clause.Length > 0))
        {
            finalQuery.Append(clause);
            finalQuery.Append(' ');
        }

        return finalQuery.ToString().Trim();
    }
}