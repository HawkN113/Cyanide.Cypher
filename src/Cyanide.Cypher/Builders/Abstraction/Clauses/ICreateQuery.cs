namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface ICreateQuery: IReturnQuery
{
    ICreateQuery Create(Action<CreateClause> configureCreate);
}