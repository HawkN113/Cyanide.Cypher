using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Admin;

public interface IShowUserQuery: IBuildQuery
{ }
public interface IAllFieldsUser
{
    /// <summary>
    /// Show all fields of the user
    /// </summary>
    /// <returns></returns>
    IFieldsCountUser WithAllFields();
}
public interface IShowAllUsers
{
    /// <summary>
    /// Show all users
    /// Sample: SHOW USERS
    /// </summary>
    /// <returns></returns>
    IAllFieldsUser WithUsers();
}
public interface ICurrentUser
{
    /// <summary>
    /// Show current user
    /// Sample: SHOW CURRENT USER
    /// </summary>
    /// <returns></returns>
    IAllFieldsUser AsCurrentUser();
}
public interface IFieldsCountUser
{
    /// <summary>
    /// Show count of the fields of the user
    /// Sample: SHOW CURRENT USER YIELD * RETURN count(*) AS count
    /// </summary>
    void WithCount();
}