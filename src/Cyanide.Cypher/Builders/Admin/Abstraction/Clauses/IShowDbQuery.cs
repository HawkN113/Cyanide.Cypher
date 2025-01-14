using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Admin;

public interface IShowDbQuery: IBuildQuery
{
}

public interface IShowAdmQueryDatabase
{
    IAllFieldsDatabase WithDatabase(string databaseName);
}
public interface IAllFieldsDatabase
{
    IFieldsCountDatabase WithAllFields();
}
public interface IDefaultDatabase
{
    void AsDefault();
}

public interface IHomeDatabase
{
    void AsHome();
}
public interface IFieldsCountDatabase
{
    void WithCount();
}