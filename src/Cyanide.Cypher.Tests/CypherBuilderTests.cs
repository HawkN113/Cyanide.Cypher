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
        var query = _queryBuilder
            .Match().Node("Person", "p").Relationship("LIVES_IN", "", RelationshipType.Direct).Node("City", "c")
            .EndMatch()
            .Where("p.age > 30")
            .Where("c.name = 'New York'")
            .Return().Property("name", "p").Property("age", "p").Property("name", "c").EndReturn()
            .Build();

        // Assert
        var expectedQuery = "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND c.name = 'New York' RETURN p.name, p.age, c.name";
        Assert.That(query, Is.EqualTo(expectedQuery));
    }
}