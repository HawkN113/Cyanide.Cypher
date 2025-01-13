using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Admin.Commands;

namespace Cyanide.Cypher.Builders.Admin;

public interface ICreateAdmQuery: IBuildQuery
{
    ICreateAdmQuery Create(Action<CreateAdmQuery> configureAdmCreate);
}


