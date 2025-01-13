using Cyanide.Cypher.Builders.Admin.Commands;

namespace Cyanide.Cypher.Builders.Admin;

public interface IAdminQuery: ICreateDbQuery
{
    ICreateDbQuery Create(Action<CreateDbQuery> configureDbCreate);
}