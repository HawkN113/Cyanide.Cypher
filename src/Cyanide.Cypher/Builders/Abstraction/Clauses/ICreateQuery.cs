namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface ICreateQuery
{
    IBuildQuery Create(Func<CreateBuilder, CreateBuilder> configureCreate);
}