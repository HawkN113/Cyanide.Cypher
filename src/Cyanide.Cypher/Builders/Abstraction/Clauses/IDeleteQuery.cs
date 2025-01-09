namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IDeleteQuery
{
    IBuildQuery Delete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
    IBuildQuery DetachDelete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
}