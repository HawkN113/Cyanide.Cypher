namespace Cyanide.Cypher.Builders.Abstraction;

public interface ICypherQueryBuilder
{
    CypherQueryBuilder Match(Func<MatchBuilder, MatchBuilder> configureMatch);
    CypherQueryBuilder Return(Func<ReturnBuilder, ReturnBuilder> configureReturn);
    CypherQueryBuilder Where(Func<WhereBuilder, WhereBuilder> conditions);
}