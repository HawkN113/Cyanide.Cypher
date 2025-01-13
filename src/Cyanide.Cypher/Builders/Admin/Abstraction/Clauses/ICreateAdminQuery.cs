using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Admin;

public interface ICreateAdmQuery: IBuildQuery
{
}

public interface ICreateAdmQueryDatabase
{
    INotExistsDatabase WithDatabase(string databaseName);
}

public interface INotExistsDatabase
{
    void IfNotExists();
}


