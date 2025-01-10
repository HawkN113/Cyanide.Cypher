namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IDeleteQuery
{
    IBuildQuery Delete(Action<DeleteBuilder> configureDelete);
    IBuildQuery DetachDelete(Action<DeleteBuilder> configureDelete);
}