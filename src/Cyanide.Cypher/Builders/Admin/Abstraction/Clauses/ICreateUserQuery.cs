using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Admin;

public interface ICreateUserQuery: IBuildQuery
{
}

public interface ICreateAdmQueryUser
{
    ISetUserPassword WithUser(string userName);
}
public interface ISetUserPassword
{
    ISetUserStatus WithPassword(string password, PasswordType type = PasswordType.PLAINTEXT, bool changeRequired = false);
}
public interface ISetUserStatus
{
    ISetUserHomeDb SetStatus(UserStatus status = UserStatus.ACTIVE);
}
public interface ISetUserHomeDb: IReplaceUser
{
    IReplaceUser SetHomeDb(string databaseName);
}

public interface IReplaceUser
{
    void Replace();
}

