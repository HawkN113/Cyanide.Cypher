namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface ICreateQuery
{
    IBuildQuery Create(Action<CreateClause> configureCreate);
}