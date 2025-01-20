using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin;

[EditionInfo(Edition.Enterprise)]
public interface IStopDbQuery: IBuildQuery
{ }

public interface IStopAdmQueryDatabase
{
    /// <summary>
    /// With database name
    /// </summary>
    void WithDatabase(string databaseName);
}