using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Admin;

public interface IShowUserQuery: IBuildQuery
{
}

public interface IAllFieldsUser
{
    IFieldsCountUser WithAllFields();
}
public interface IShowAllUsers
{
    IAllFieldsUser WithUsers();
}
public interface ICurrentUser
{
    IAllFieldsUser AsCurrentUser();
}

public interface IFieldsCountUser
{
    void WithCount();
}