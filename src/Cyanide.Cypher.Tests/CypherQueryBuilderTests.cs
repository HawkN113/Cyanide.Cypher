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

    #region MATCH
    
    [Test]
    public void Translate_CypherQueryWithMatch_DirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node("City", "c")
            )
            .Return(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    [Test]
    public void Translate_CypherQueryWithMatch_InDirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.InDirect)
                    .Node("City", "c")
            )
            .Return(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)<-[:LIVES_IN]-(c:City) RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }

    [Test]
    public void Translate_CypherQueryWithMatch_BiDirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.BiDirect)
                    .Node("City", "c")
            )
            .Return(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)->[:LIVES_IN]<-(c:City) RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }

    [Test]
    public void Translate_CypherQueryWithMatch_UnDirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.UnDirect)
                    .Node("City", "c")
            )
            .Return(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)<-[:LIVES_IN]->(c:City) RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    [Test]
    public void Translate_CypherQueryWithMatch_NonDirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN")
                    .Node("City", "c")
            )
            .Return(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]-(c:City) RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    [Test]
    public void Translate_CypherQueryWithMatch_NonDirectRelationWithAlias_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", alias:"x")
                    .Node("City", "c")
            )
            .Return(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[x:LIVES_IN]-(c:City) RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }

    #endregion
    
    #region WHERE
    
    [Test]
    public void Translate_CypherQueryWithWhereQuery_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node("City", "c")
            )
            .Where(q => q.Query("p.age > 30"))
            .Return(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    [Test]
    public void Translate_CypherQueryWithWhere_AND_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node("City", "c")
            )
            .Where(q => q.Query("p.age > 30").And(q1=>q1.Query("b.city=\"New York\"")))
            .Return(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city=\"New York\" RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    [Test]
    public void Translate_CypherQueryWithWhere_OR_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node("City", "c")
            )
            .Where(q => q.Query("p.age > 30").Or(q1=>q1.Query("b.city=\"New York\"")))
            .Return(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 OR b.city=\"New York\" RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    [Test]
    public void Translate_CypherQueryWithWhere_XOR_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node("City", "c")
            )
            .Where(q => q.Query("p.age > 30").Xor(q1=>q1.Query("b.city=\"New York\"")))
            .Return(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 XOR b.city=\"New York\" RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    [Test]
    public void Translate_CypherQueryWithWhere_NOT_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node("City", "c")
            )
            .Where(q => q.Query("p.age > 30").Not(q1 => q1.Query("b.city=\"New York\"")))
            .Return(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 NOT b.city=\"New York\" RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    [Test]
    public void Translate_CypherQueryWithWhere_IS_NOT_NULL_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node("Person", "p")
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node("City", "c")
            )
            .Where(q => q.Query("p.age > 30").And().IsNotNull("b.city"))
            .Return(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city IS NOT NULL RETURN p.name, c.name";
        Assert.That(resultQuery, Is.EqualTo(expectedQuery));
    }
    
    #endregion
}