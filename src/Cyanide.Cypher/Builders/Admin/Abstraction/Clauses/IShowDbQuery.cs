using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Admin;

public interface IShowDbQuery: IBuildQuery
{ }
public interface IShowAdmQueryDatabase
{
    /// <summary>
    /// With database name <br/>
    /// Sample: SHOW DATABASE db
    /// </summary>
    /// <param name="databaseName"></param>
    /// <returns></returns>
    IAllFieldsDatabase WithDatabase(string databaseName);
}
public interface IAllFieldsDatabase
{
    /// <summary>
    /// Show all fields of the database
    /// Sample: SHOW DATABASE db YIELD *
    /// </summary>
    /// <returns></returns>
    IFieldsCountDatabase WithAllFields();
}
public interface IShowAllDatabases
{
    /// <summary>
    /// Show all database
    /// Sample: SHOW DATABASES
    /// </summary>
    void WithDatabases();
}
public interface IDefaultDatabase
{
    /// <summary>
    /// Show as default database
    /// Sample: SHOW DEFAULT DATABASE
    /// </summary>
    void AsDefaultDatabase();
}
public interface IHomeDatabase
{
    /// <summary>
    /// Show as home database
    /// Sample: SHOW HOME DATABASE
    /// </summary>
    void AsHomeDatabase();
}
public interface IFieldsCountDatabase
{
    /// <summary>
    /// Show counts of fields of the database
    /// Sample: SHOW DATABASE db YIELD * RETURN count(*) AS count
    /// </summary>
    void WithCount();
}