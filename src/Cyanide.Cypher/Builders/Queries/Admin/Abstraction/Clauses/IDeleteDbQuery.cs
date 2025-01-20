using Cyanide.Cypher.Attributes;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin;

[EditionInfo(Edition.Enterprise)]
public interface IDeleteDbQuery: IBuildQuery
{ }

public interface IDeleteAdmQueryDatabase
{
    /// <summary>
    /// With database name
    /// </summary>
    void WithDatabase(string databaseName);
}