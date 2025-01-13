using Cyanide.Cypher.Builders.Admin;
using Cyanide.Cypher.Builders.Admin.Commands;

namespace Cyanide.Cypher.Builders;

public interface IAdminQuery: ICreateDbQuery, ICreateUserQuery
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
    /// Sample: CREATE OR REPLACE USER name <br/>
    ///         <tab/> SET PLAINTEXT PASSWORD 'password' <br/>
    /// </summary>
    /// <param name="configureUserCreate"></param>
    /// <returns></returns>
    ICreateUserQuery Create(Action<CreateUserQuery> configureUserCreate);
}