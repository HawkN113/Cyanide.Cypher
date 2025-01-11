namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

public interface IDeleteQuery: IBuildQuery
{
    IDeleteQuery Delete(Action<DeleteClause> configureDelete);
    IDeleteQuery DetachDelete(Action<DeleteClause> configureDelete);
}