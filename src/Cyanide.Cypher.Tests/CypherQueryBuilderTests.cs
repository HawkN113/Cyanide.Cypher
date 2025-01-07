using Cyanide.Cypher.Builders;

namespace Cyanide.Cypher.Tests;

public class CypherQueryBuilderTests
{
    private readonly CypherQueryBuilder _queryBuilder = new();

    #region MATCH

    [Fact]
    public void Translate_CypherQueryWith_MATCH_DirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Select(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_MATCH_InDirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.InDirect)
                    .Node(new Entity("City", "c"))
            )
            .Select(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)<-[:LIVES_IN]-(c:City) RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_MATCH_BiDirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.BiDirect)
                    .Node(new Entity("City", "c"))
            )
            .Select(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)->[:LIVES_IN]<-(c:City) RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_MATCH_UnDirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.UnDirect)
                    .Node(new Entity("City", "c"))
            )
            .Select(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)<-[:LIVES_IN]->(c:City) RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_MATCH_NonDirectRelation_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN")
                    .Node(new Entity("City", "c"))
            )
            .Select(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]-(c:City) RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_MATCH_NonDirectRelationWithAlias_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", alias: "x")
                    .Node(new Entity("City", "c"))
            )
            .Select(q =>
                q.Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[x:LIVES_IN]-(c:City) RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_MATCH_AndPropertyAndAlias_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "a"), [
                    new Property() { Label = "name", Value = "'Martin Sheen'" }
                ])
            )
            .Select(q =>
                q.Property("name", "a")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (a:Person {name: 'Martin Sheen'}) RETURN a.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_MATCH_AndPropertyWithoutAlias_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Select(q =>
                q.Property("name", "person", "person")
            )
            .Match(q =>
                q.Node(new Entity("Movie", ""), [
                        new Property() { Label = "title", Value = "'Wall Street'" }
                    ])
                    .Relationship("ACTED_IN|DIRECTED", RelationshipType.InDirect)
                    .Node(new Entity("Person", "person"))
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (:Movie {title: 'Wall Street'})<-[:ACTED_IN|DIRECTED]-(person:Person) RETURN person.name AS person";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWithMulti_MATCH_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Select(q =>
                q.Property("name", "a").Property("r")
            )
            .Match(q =>
                q.Node(new Entity("Person", "a"), [
                    new Property() { Label = "name", Value = "'Martin Sheen'" }
                ])
            )
            .Match(q =>
                q.Node(new Entity("a"))
                    .Relationship("DIRECTED", RelationshipType.Direct, "r")
                    .EmptyNode()
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (a:Person {name: 'Martin Sheen'}) MATCH (a)-[r:DIRECTED]->() RETURN a.name, r";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_OPTIONAL_MATCH_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Select(q =>
                q.Property("name", "a").Property("r")
            )
            .Match(q =>
                q.Node(new Entity("Person", "a"), [
                    new Property() { Label = "name", Value = "'Martin Sheen'" }
                ])
            )
            .OptionalMatch(q =>
                q.Node(new Entity("a"))
                    .Relationship("DIRECTED", RelationshipType.Direct, "r")
                    .EmptyNode()
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (a:Person {name: 'Martin Sheen'}) OPTIONAL MATCH (a)-[r:DIRECTED]->() RETURN a.name, r";
        Assert.Equal(resultQuery, expectedQuery);
    }

    #endregion

    #region WHERE

    [Fact]
    public void Translate_CypherQueryWith_WHERE_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30"))
            .Select(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_WHERE_AND_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").And(q1 => q1.Query("b.city=\"New York\"")))
            .Select(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city=\"New York\" RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_WHERE_OR_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").Or(q1 => q1.Query("b.city=\"New York\"")))
            .Select(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 OR b.city=\"New York\" RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_WHERE_XOR_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").Xor(q1 => q1.Query("b.city=\"New York\"")))
            .Select(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 XOR b.city=\"New York\" RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_WHERE_NOT_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").Not(q1 => q1.Query("b.city=\"New York\"")))
            .Select(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 NOT b.city=\"New York\" RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_WHERE_IS_NOT_NULL_Query_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").And(q => q.IsNotNull("b.city")))
            .Select(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city IS NOT NULL RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    #endregion

    #region CREATE

    [Fact]
    public void Translate_CypherQueryWith_CREATE_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "charlie"), [
                    new Property() { Label = "name", Value = "'Charlie Sheen'" }
                ])
            )
            .Create(q => q.Node(new Entity("Actor", "charlie")))
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (charlie:Person {name: 'Charlie Sheen'}) CREATE (charlie:Actor)";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_CREATE_Where_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "person"))
            )
            .Where(q => q.IsNotNull("person.name"))
            .Create(q => q.Node(new Entity("Person", "anotherPerson"), [
                new Property() { Label = "name", Value = "person.name" }
            ]))
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (person:Person) WHERE person.name IS NOT NULL CREATE (anotherPerson:Person {name: person.name})";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_CypherQueryWith_CREATE_MultiProperties_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q => q.Node(new Entity("Person", "n"), [
                new Property() { Label = "name", Value = "'Andy'" },
                new Property() { Label = "title", Value = "'Developer'" }
            ]))
            .Build();

        // Assert
        var expectedQuery =
            "CREATE (n:Person {name: 'Andy', title: 'Developer'})";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    [Fact]
    public void Translate_CypherQueryWith_CREATE_FullPath_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q => 
                q.Node(new Entity("Person", "andy"), [new Property() { Label = "name", Value = "'Andy'" }])
                    .Relationship(new Entity("WORKS_AT"), RelationshipType.Direct,null, new Entity("neo"))
                .Relationship("WORKS_AT", RelationshipType.InDirect)
                .Node(new Entity("Person", "michael"), [new Property() { Label = "name", Value = "'Michael'" }])
            )
            .Select(q=>q.Property("andy").Property("michael") )
            .Build();

        // Assert
        var expectedQuery =
            "CREATE (andy:Person {name: 'Andy'})-[:WORKS_AT]->(neo)<-[:WORKS_AT]-(michael:Person {name: 'Michael'}) RETURN andy, michael";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    [Fact]
    public void Translate_CypherQueryWith_CREATE_MultiNodes_ReturnsCorrectSql()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q => 
                q.Node(new Entity("Person","keanu"), [new Property() { Label = "name", Value = "'Keanu Reever'" }])
                    .Node(new Entity("Person","laurence"), [new Property() { Label = "name", Value = "'Laurence Fishburne'" }])
                    .Relationship(new Entity("ACTED_IN"), RelationshipType.Direct, new Entity("keanu"), new Entity("theMatrix"))
                    .Relationship(new Entity("ACTED_IN"), RelationshipType.Direct, new Entity("laurence"), new Entity("theMatrix"))
            )
            .Build();

        // Assert
        var expectedQuery =
            "CREATE (keanu:Person {name: 'Keanu Reever'}), (laurence:Person {name: 'Laurence Fishburne'}), (keanu)-[:ACTED_IN]->(theMatrix), (laurence)-[:ACTED_IN]->(theMatrix)";
        Assert.Equal(resultQuery, expectedQuery);
    }

    #endregion
}