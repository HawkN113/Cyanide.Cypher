using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin;

public interface ICreateUserQuery: IBuildQuery
{ }
public interface ICreateAdmQueryUser
{
    /// <summary>
    /// With user's name
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    ISetUserPassword WithUser(string userName);
}
public interface ISetUserPassword
{
    /// <summary>
    /// Set user's password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="type">PLAINTEXT or ENCRYPTED</param>
    /// <param name="changeRequired">Should change the password or not</param>
    /// <returns></returns>
    ISetUserStatus WithPassword(string password, PasswordType type = PasswordType.Plaintext, bool changeRequired = false);
}
public interface ISetUserStatus
{
    /// <summary>
    /// Set user's status
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    ISetUserHomeDb SetStatus(UserStatus status = UserStatus.Active);
}
public interface ISetUserHomeDb: IReplaceUser
{
    /// <summary>
    /// Set for home database
    /// </summary>
    /// <param name="databaseName"></param>
    /// <returns></returns>
    IReplaceUser SetHomeDb(string databaseName);
}

public interface IReplaceUser
{
    /// <summary>
    /// Replace existing user
    /// </summary>
    void Replace();
}

