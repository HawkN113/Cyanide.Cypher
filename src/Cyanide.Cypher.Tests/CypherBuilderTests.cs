using Cyanide.Cypher.Builders;

namespace Cyanide.Cypher.Tests;

public class CypherBuilderTests
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
        // Arrange


        // Act
        var query = _queryBuilder.Match("(p:Person)-[:LIVES_IN]->(c:City)")
            .Where("p.age > 30")
            .Where("c.name = 'New York'")
            .Return("p.name", "p.age", "c.name")
            .Build();

        // Assert
        var expectedQuery = "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND c.name = 'New York' RETURN p.name, p.age, c.name";
        Assert.That(query, Is.EqualTo(expectedQuery));
    }
}