using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Admin;

public interface ICreateDbQuery: IBuildQuery
{
}

public interface ICreateAdmQueryDatabase
{
    INotExistsDatabase WithDatabase(string databaseName);
}

public interface INotExistsDatabase: IReplaceDatabase
{
    IReplaceDatabase IfNotExists();
}

public interface IReplaceDatabase
{
    void Replace();
}


