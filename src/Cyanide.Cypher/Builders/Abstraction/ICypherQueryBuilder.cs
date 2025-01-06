namespace Cyanide.Cypher.Builders.Abstraction;

public interface ICypherQueryBuilder
{
    CypherQueryBuilder Create(Func<CreateBuilder, CreateBuilder> configureCreate);
    CypherQueryBuilder Match(Func<MatchBuilder, MatchBuilder> configureMatch);
    CypherQueryBuilder OptionalMatch(Func<OptMatchBuilder, OptMatchBuilder> configureOptMatch);
    CypherQueryBuilder Select(Func<SelectBuilder, SelectBuilder> configureReturn);
    CypherQueryBuilder Where(Func<WhereBuilder, WhereBuilder> conditions);
}