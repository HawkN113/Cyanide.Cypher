namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IDeleteQuery
{
    IBuildQuery Delete(Action<DeleteClause> configureDelete);
    IBuildQuery DetachDelete(Action<DeleteClause> configureDelete);
}