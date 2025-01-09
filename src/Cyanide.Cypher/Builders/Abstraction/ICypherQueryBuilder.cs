namespace Cyanide.Cypher.Builders.Abstraction;

public interface ICypherQueryBuilder
{
    CypherQueryBuilder Create(Func<CreateBuilder, CreateBuilder> configureCreate);
    CypherQueryBuilder Match(Func<MatchBuilder, MatchBuilder> configureMatch);
    CypherQueryBuilder Delete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
    CypherQueryBuilder DetachDelete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
    CypherQueryBuilder OptionalMatch(Func<OptMatchBuilder, OptMatchBuilder> configureOptMatch);
    CypherQueryBuilder Select(Func<SelectBuilder, SelectBuilder> configureReturn);
    CypherQueryBuilder Where(Func<WhereBuilder, WhereBuilder> conditions);
    CypherQueryBuilder OrderBy(Func<OrderByBuilder, OrderByBuilder> configureOrderBy);
}