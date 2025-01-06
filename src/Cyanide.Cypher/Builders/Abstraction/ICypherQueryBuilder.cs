namespace Cyanide.Cypher.Builders.Abstraction;

public interface ICypherQueryBuilder
{
    CypherQueryBuilder Match(Action<MatchBuilder> configureMatch);
    CypherQueryBuilder Return(Action<ReturnBuilder> configureReturn);
    CypherQueryBuilder Where(Action<WhereBuilder> configureWhere);
    string Build();
}