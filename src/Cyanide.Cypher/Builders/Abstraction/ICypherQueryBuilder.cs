namespace Cyanide.Cypher.Builders.Abstraction;

public interface ICypherQueryBuilder
{
    CypherQueryBuilder Match(Func<MatchBuilder, MatchBuilder> configureMatch);
    CypherQueryBuilder Select(Func<SelectBuilder, SelectBuilder> configureReturn);
    CypherQueryBuilder Where(Func<WhereBuilder, WhereBuilder> conditions);
}