using System.Text;
using Cyanide.Cypher.Builders.Queries.Admin.Commands;
using Cyanide.Cypher.Builders.Base;
using Cyanide.Cypher.Builders.Queries.Admin;
using Cyanide.Cypher.Extensions;

namespace Cyanide.Cypher.Builders.AdminQuery;

internal sealed class CypherAdminBuilder() : BaseQueryBuilder(
    Enum.GetValues<AdminClauseKeys>().ToDictionary(key => key.ToString(), _ => new StringBuilder())), IAdminQuery
{
    /// <summary>
    /// CREATE DATABASE clause for admin management <br/>
    /// CREATE OR REPLACE DATABASE clause for admin management <br/>
    /// Sample: CREATE DATABASE db
    /// For Enterprise Edition
    /// </summary>
    /// <param name="configureDbCreate"></param>
    /// <returns></returns>
    public ICreateDbQuery Create(Action<CreateDbQuery> configureDbCreate) =>
        ConfigureQueryBuilder<ICreateDbQuery, CreateDbQuery>(
            AdminClauseKeys.CreateDb.GetDescription(), 
            configureDbCreate);

    /// <summary>
    /// CREATE USER clause for admin management <br/>
    /// CREATE OR USER clause for admin management <br/>
    /// Sample: CREATE OR REPLACE USER name <br/>
    ///         <tab/> SET PLAINTEXT PASSWORD 'password' <br/>
    /// </summary>
    /// <param name="configureUserCreate"></param>
    /// <returns></returns>
    public ICreateUserQuery Create(Action<CreateUserQuery> configureUserCreate) =>
        ConfigureQueryBuilder<ICreateUserQuery, CreateUserQuery>(
            AdminClauseKeys.CreateUser.GetDescription(),
            configureUserCreate);

    /// <summary>
    /// SHOW DATABASE(S) clause for admin management <br/>
    /// Sample: SHOW DATABASE db YIELD *
    /// </summary>
    /// <param name="configureDbShow"></param>
    public IShowDbQuery Show(Action<ShowDbQuery> configureDbShow) => 
        ConfigureQueryBuilder<IShowDbQuery, ShowDbQuery>(
        AdminClauseKeys.ShowDb.GetDescription(),
        configureDbShow);

    /// <summary>
    /// SHOW USER(S) clause for admin management <br/>
    /// Sample: SHOW CURRENT USER db YIELD *
    /// </summary>
    /// <param name="configureUserShow"></param>
    public IShowUserQuery Show(Action<ShowUserQuery> configureUserShow) =>
        ConfigureQueryBuilder<IShowUserQuery, ShowUserQuery>(
            AdminClauseKeys.ShowUser.GetDescription(), 
            configureUserShow);

    /// <summary>
    /// ALTER DATABASE clause for admin management <br/>
    /// Sample: ALTER DATABASE db SET ACCESS READ ONLY <br/>
    /// Sample: ALTER DATABASE db SET ACCESS READ WRITE
    /// </summary>
    /// <param name="configureDbUpdate"></param>
    public IUpdateDbQuery Update(Action<UpdateDbQuery> configureDbUpdate)=>
        ConfigureQueryBuilder<IUpdateDbQuery, UpdateDbQuery>(
            AdminClauseKeys.UpdateDb.GetDescription(), 
            configureDbUpdate);

    /// <summary>
    /// START DATABASE clause for admin management <br/>
    /// Sample: START DATABASE db
    /// </summary>
    /// <param name="configureDbStart"></param>
    public IStartDbQuery Start(Action<StartDbQuery> configureDbStart) =>
        ConfigureQueryBuilder<IStartDbQuery, StartDbQuery>(
            AdminClauseKeys.StartDb.GetDescription(),
            configureDbStart);
    
    /// <summary>
    /// STOP DATABASE clause for admin management <br/>
    /// Sample: STOP DATABASE db
    /// </summary>
    /// <param name="configureDbStop"></param>
    public IStopDbQuery Stop(Action<StopDbQuery> configureDbStop) =>
        ConfigureQueryBuilder<IStopDbQuery, StopDbQuery>(
            AdminClauseKeys.StopDb.GetDescription(), configureDbStop);

    /// <summary>
    /// DELETE DATABASE clause for admin management <br/>
    /// Sample: DELETE DATABASE db
    /// </summary>
    /// <param name="configureDbDelete"></param>
    public IDeleteDbQuery Delete(Action<DeleteDbQuery> configureDbDelete) =>
        ConfigureQueryBuilder<IDeleteDbQuery, DeleteDbQuery>(
            AdminClauseKeys.DeleteDb.GetDescription(), configureDbDelete);

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