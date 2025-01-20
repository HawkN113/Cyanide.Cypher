using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin;

[EditionInfo(Edition.Enterprise)]
public interface IStartDbQuery: IBuildQuery
{ }

public interface IStartAdmQueryDatabase
{
    /// <summary>
    /// With database name
    /// </summary>
    void WithDatabase(string databaseName);
}