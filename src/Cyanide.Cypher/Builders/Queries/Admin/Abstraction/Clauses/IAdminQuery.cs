using Cyanide.Cypher.Builders.Queries.Admin.Commands;

namespace Cyanide.Cypher.Builders.Queries.Admin;

public interface IAdminQuery: ICreateDbQuery, ICreateUserQuery, IShowDbQuery, IShowUserQuery
{
    /// <summary>
    /// CREATE DATABASE clause for admin management <br/>
    /// CREATE OR REPLACE DATABASE clause for admin management <br/>
    /// Sample: CREATE DATABASE db
    /// </summary>
    /// <param name="configureDbCreate"></param>
    /// <returns></returns>
    ICreateDbQuery Create(Action<CreateDbQuery> configureDbCreate);
    
    /// <summary>
    /// CREATE USER clause for admin management <br/>
    /// CREATE OR USER DATABASE clause for admin management <br/>
    /// Sample: CREATE OR REPLACE USER usr1 <br/>
    /// </summary>
    /// <param name="configureUserCreate"></param>
    /// <returns></returns>
    ICreateUserQuery Create(Action<CreateUserQuery> configureUserCreate);
    
    /// <summary>
    /// SHOW DATABASE(S) clause for admin management <br/>
    /// Sample: SHOW DATABASE db YIELD *
    /// </summary>
    /// <param name="configureDbShow"></param>
    /// <returns></returns>
    IShowDbQuery Show(Action<ShowDbQuery> configureDbShow);
    
    /// <summary>
    /// SHOW USER(S) clause for admin management <br/>
    /// Sample: SHOW CURRENT USER usr1 YIELD *
    /// </summary>
    /// <param name="configureUserShow"></param>
    /// <returns></returns>
    IShowUserQuery Show(Action<ShowUserQuery> configureUserShow);
}