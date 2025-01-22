using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Queries.Admin.Commands;

namespace Cyanide.Cypher.Builders.Queries.Admin;

public interface IAdminQuery: ICreateDbQuery, ICreateUserQuery, IShowDbQuery, IShowUserQuery, IUpdateDbQuery, IStartDbQuery, IStopDbQuery,IDeleteDbQuery
{
    /// <summary>
    /// CREATE DATABASE clause for admin management <br/>
    /// CREATE OR REPLACE DATABASE clause for admin management <br/>
    /// Sample: CREATE DATABASE db
    /// </summary>
    /// <param name="configureDbCreate"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    [EditionInfo(Edition.Enterprise)]
    ICreateDbQuery Create(Action<CreateDbQuery> configureDbCreate);
    
    /// <summary>
    /// CREATE USER clause for admin management <br/>
    /// CREATE OR USER DATABASE clause for admin management <br/>
    /// Sample: CREATE OR REPLACE USER usr1 <br/>
    /// </summary>
    /// <param name="configureUserCreate"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    ICreateUserQuery Create(Action<CreateUserQuery> configureUserCreate);
    
    /// <summary>
    /// SHOW DATABASE(S) clause for admin management <br/>
    /// Sample: SHOW DATABASE db YIELD *
    /// </summary>
    /// <param name="configureDbShow"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    IShowDbQuery Show(Action<ShowDbQuery> configureDbShow);
    
    /// <summary>
    /// SHOW USER(S) clause for admin management <br/>
    /// Sample: SHOW CURRENT USER usr1 YIELD *
    /// </summary>
    /// <param name="configureUserShow"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    IShowUserQuery Show(Action<ShowUserQuery> configureUserShow);
    
    /// <summary>
    /// ALTER DATABASE clause for admin management <br/>
    /// Sample: ALTER DATABASE db SET ACCESS READ ONLY
    /// </summary>
    /// <param name="configureDbUpdate"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    [EditionInfo(Edition.Enterprise)]
    IUpdateDbQuery Update(Action<UpdateDbQuery> configureDbUpdate);
    
    /// <summary>
    /// START DATABASE clause for admin management <br/>
    /// Sample: START DATABASE db
    /// </summary>
    /// <param name="configureDbStart"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    [EditionInfo(Edition.Enterprise)]
    IStartDbQuery Start(Action<StartDbQuery> configureDbStart);
    
    /// <summary>
    /// STOP DATABASE clause for admin management <br/>
    /// Sample: STOP DATABASE db
    /// </summary>
    /// <param name="configureDbStop"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    [EditionInfo(Edition.Enterprise)]
    IStopDbQuery Stop(Action<StopDbQuery> configureDbStop);
    
    /// <summary>
    /// DELETE DATABASE clause for admin management <br/>
    /// Sample: DELETE DATABASE db
    /// </summary>
    /// <param name="configureDbDelete"></param>
    /// <returns></returns>
    [VersionInfo("4.4")]
    [EditionInfo(Edition.Enterprise)]
    IDeleteDbQuery Delete(Action<DeleteDbQuery> configureDbDelete);
    
}