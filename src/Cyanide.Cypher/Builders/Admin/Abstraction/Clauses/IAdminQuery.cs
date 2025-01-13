using Cyanide.Cypher.Builders.Admin.Commands;

namespace Cyanide.Cypher.Builders.Admin;

public interface IAdminQuery: ICreateAdmQuery
{
    ICreateAdmQuery Create(Action<CreateAdmQuery> configureAdmCreate);
}