using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin;

[EditionInfo(Edition.Enterprise)]
public interface IUpdateDbQuery: IBuildQuery
{ }

public interface IUpdateAdmQueryDatabase
{
    /// <summary>
    /// With database name
    /// </summary>
    ISetAccess WithDatabase(string databaseName);
}

public interface ISetAccess : ISetAccessReadOnly, ISetAccessReadWrite
{ }

public interface ISetAccessReadOnly
{
    /// <summary>
    /// Set readonly permission
    /// Sample: ALTER DATABASE customers SET ACCESS READ ONLY
    /// </summary>
    void SetAccessReadOnly();
}

public interface ISetAccessReadWrite
{
    /// <summary>
    /// Set read and write permissions
    /// Sample: ALTER DATABASE customers SET ACCESS READ WRITE
    /// </summary>
    void SetAccessReadWrite();
}