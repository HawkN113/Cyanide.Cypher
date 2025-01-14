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
public interface IShowAllDatabases
{
    void WithDatabases();
}
public interface IDefaultDatabase
{
    void AsDefaultDatabase();
}

public interface IHomeDatabase
{
    void AsHomeDatabase();
}
public interface IFieldsCountDatabase
{
    void WithCount();
}