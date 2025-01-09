using Cyanide.Cypher.Builders;

namespace Cyanide.Cypher.Tests;

public class CypherQueryBuilderTests
{
    private readonly CypherQueryBuilder _queryBuilder = new();

    #region MATCH

    [Fact]
    public void Translate_With_MATCH_DirectRelation_ReturnsCorrectCypherQuery()
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
    public void Translate_With_MATCH_InDirectRelation_ReturnsCorrectCypherQuery()
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
    public void Translate_With_MATCH_BiDirectRelation_ReturnsCorrectCypherQuery()
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
    public void Translate_With_MATCH_UnDirectRelation_ReturnsCorrectCypherQuery()
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
    public void Translate_With_MATCH_NonDirectRelation_ReturnsCorrectCypherQuery()
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
    public void Translate_With_MATCH_NonDirectRelationWithAlias_ReturnsCorrectCypherQuery()
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
    public void Translate_With_MATCH_AndPropertyAndAlias_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "a", [new Property("name", "'Martin Sheen'")]))
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
    public void Translate_With_MATCH_AndPropertyWithoutAlias_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Movie", "", [new Property("title", "'Wall Street'")]))
                    .Relationship("ACTED_IN|DIRECTED", RelationshipType.InDirect)
                    .Node(new Entity("Person", "person"))
            )
            .Select(q =>
                q.Property("name", "person", "person")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (:Movie {title: 'Wall Street'})<-[:ACTED_IN|DIRECTED]-(person:Person) RETURN person.name AS person";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_With_Multi_MATCH_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "a",[new Property("name","'Martin Sheen'")]))
            )
            .Match(q =>
                q.Node(new Entity("a"))
                    .Relationship("DIRECTED", RelationshipType.Direct, "r")
                    .EmptyNode()
            )
            .Select(q =>
                q.Property("name", "a").Property("r")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (a:Person {name: 'Martin Sheen'}) MATCH (a)-[r:DIRECTED]->() RETURN a.name, r";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_With_OPTIONAL_MATCH_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "a", [new Property("name", "'Martin Sheen'")]))
            )
            .OptionalMatch(q =>
                q.Node(new Entity("a"))
                    .Relationship("DIRECTED", RelationshipType.Direct, "r")
                    .EmptyNode()
            )
            .Select(q =>
                q.Property("name", "a").Property("r")
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
    public void Translate_With_WHERE_Query_ReturnsCorrectCypherQuery()
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
    public void Translate_With_WHERE_AND_Query_ReturnsCorrectCypherQuery()
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
    public void Translate_With_WHERE_OR_Query_ReturnsCorrectCypherQuery()
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
    public void Translate_With_WHERE_XOR_Query_ReturnsCorrectCypherQuery()
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
    public void Translate_With_WHERE_NOT_Query_ReturnsCorrectCypherQuery()
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
    public void Translate_With_WHERE_IS_NOT_NULL_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").And(q1 => q1.IsNotNull("b.city")))
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
    
    [Fact]
    public void Translate_With_WHERE_IS_NULL_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "p"))
                    .Relationship("LIVES_IN", RelationshipType.Direct)
                    .Node(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").And(q1 => q1.IsNull("b.city")))
            .Select(q =>
                q
                    .Property("name", "p")
                    .Property("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city IS NULL RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    #endregion

    #region CREATE

    [Fact]
    public void Translate_With_CREATE_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "charlie",[new Property("name","'Charlie Sheen'")]))
            )
            .Create(q => q.Node(new Entity("Actor", "charlie")))
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (charlie:Person {name: 'Charlie Sheen'}) CREATE (charlie:Actor)";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_With_CREATE_Where_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "person"))
            )
            .Where(q => q.IsNotNull("person.name"))
            .Create(q => q.Node(new Entity("Person", "anotherPerson", [new Property("name", "person.name")])))
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (person:Person) WHERE person.name IS NOT NULL CREATE (anotherPerson:Person {name: person.name})";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_With_CREATE_MultiProperties_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.Node(new Entity("Person", "n",
                    [new Property("name", "'Andy'"), 
                        new Property("title", "'Developer'")])))
            .Build();

        // Assert
        var expectedQuery =
            "CREATE (n:Person {name: 'Andy', title: 'Developer'})";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    [Fact]
    public void Translate_With_CREATE_FullPath_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.Relationship(new Entity("WORKS_AT"), RelationshipType.Direct, 
                        new Entity("Person", "andy", [new Property("name", "'Andy'")]), 
                        new Entity("neo"))
                    .Relationship("WORKS_AT", RelationshipType.InDirect)
                    .Node(new Entity("Person", "michael", [new Property("name", "'Michael'")]))
            )
            .Select(q => q.Property("andy").Property("michael"))                              
            .Build();

        // Assert
        var expectedQuery =
            "CREATE (andy:Person {name: 'Andy'})-[:WORKS_AT]->(neo)<-[:WORKS_AT]-(michael:Person {name: 'Michael'}) RETURN andy, michael";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    [Fact]
    public void Translate_With_CREATE_MultiNodes_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.Node(new Entity("Person", "keanu", [new Property("name", "'Keanu Reever'")]))
                    .Node(new Entity("Person", "laurence", [new Property("name", "'Laurence Fishburne'")]))
                    .Relationship(new Entity("ACTED_IN"), RelationshipType.Direct, new Entity("keanu"),
                        new Entity("theMatrix"))
                    .Relationship(new Entity("ACTED_IN"), RelationshipType.Direct, new Entity("laurence"),
                        new Entity("theMatrix"))
            )
            .Build();

        // Assert
        var expectedQuery =
            "CREATE (keanu:Person {name: 'Keanu Reever'}), (laurence:Person {name: 'Laurence Fishburne'}), (keanu)-[:ACTED_IN]->(theMatrix), (laurence)-[:ACTED_IN]->(theMatrix)";
        Assert.Equal(resultQuery, expectedQuery);
    }

    #endregion
    
    #region DELETE
    
    [Fact]
    public void Translate_With_DELETE_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Relationship(new Entity("ACTED_IN", "r"), RelationshipType.Direct, 
                        new Entity("Person", "n", [new Property("name", "'Laurence Fishburne'")]), 
                        new Entity(string.Empty))
            )
            .Delete(q => q.Node("r"))
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (n:Person {name: 'Laurence Fishburne'})-[r:ACTED_IN]->() DELETE r";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    [Fact]
    public void Translate_With_DETACH_DELETE_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("Person", "n", [new Property("name", "'Carrie-Anne Moss'")]))
            )
            .DetachDelete(q => q.Node("n"))
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (n:Person {name: 'Carrie-Anne Moss'}) DETACH DELETE n";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    #endregion
    
    #region ORDER BY
    
    [Fact]
    public void Translate_With_ORDER_BY_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("n", ""))
            )
            .Select(q => q.Property("name", "n").Property("age", "n"))
            .OrderBy(q => q.Property("name", "n").Property("age", "n"))
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (n) RETURN n.name, n.age ORDER BY n.name, n.age";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    [Fact]
    public void Translate_With_ORDER_BY_DESC_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.Node(new Entity("n", ""))
            )
            .Select(q => q.Property("name", "n").Property("age", "n"))
            .OrderBy(q => q.Property("name", "n").Desc())
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (n) RETURN n.name, n.age ORDER BY n.name DESC";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    #endregion
}