namespace Cyanide.Cypher.Builders.Abstraction;

public interface ICypherQueryBuilder
{
    CypherQueryBuilder Create(Func<CreateBuilder, CreateBuilder> configureCreate);
    CypherQueryBuilder Match(
        Func<MatchBuilder, MatchBuilder> configureMatch,
        Func<OptMatchBuilder, OptMatchBuilder>? configureOptMatch = null);
    CypherQueryBuilder Delete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
    CypherQueryBuilder DetachDelete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
    CypherQueryBuilder Select(
        Func<SelectBuilder, SelectBuilder> configureReturn, 
        Func<OrderByBuilder, OrderByBuilder>? configureOrderBy = null);
    CypherQueryBuilder Where(Func<WhereBuilder, WhereBuilder> conditions);
}