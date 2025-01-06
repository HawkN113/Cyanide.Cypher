using Cyanide.Cypher.Builders;

namespace Cyanide.Cypher.Tests;

public class CypherQueryBuilderTests
{
    private CypherQueryBuilder _queryBuilder;
    
    [SetUp]
    public void SetUp()
    {
        _queryBuilder = new CypherQueryBuilder();
    }

    [Test]
    public void Translate_CypherQueryWithMatch_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", "", RelationshipType.Direct)
                    .Node("City", "c")
                    .EndMatch()
            )
            .Return(q =>
                q.Property("name", "p")
                    .Property("name", "c")
                    .EndReturn()
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }

    [Test]
    public void Translate_CypherQueryWithWhereQuery_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", "", RelationshipType.Direct)
                    .Node("City", "c")
                    .EndMatch()
            )
            .Where(q =>
                q.Query("p.age > 30")
                    .EndWhere()
            )
            .Return(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
                    .EndReturn()
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
}