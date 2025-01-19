using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Admin;

public interface ICreateDbQuery: IBuildQuery
{ }
public interface ICreateAdmQueryDatabase
{
    /// <summary>
    /// With database name
    /// </summary>
    INotExistsDatabase WithDatabase(string databaseName);
}
public interface INotExistsDatabase: IReplaceDatabase
{
    /// <summary>
    /// Create a database if doesn't exist
    /// Use CREATE DATABASE [IF NOT EXISTS] clause to ensure the database is only created if it does not already exist, or use CREATE OR REPLACE DATABASE to create a new database, replacing the existing one if it already exists.
    /// </summary>
    IReplaceDatabase IfNotExists();
}
public interface IReplaceDatabase
{
    /// <summary>
    /// Replace existing database
    /// Use CREATE DATABASE with the [IF NOT EXISTS] clause to ensure the database is only created if it does not already exist, or use CREATE OR REPLACE DATABASE to create a new database, replacing the existing one if it already exists.
    /// </summary>
    void Replace();
}