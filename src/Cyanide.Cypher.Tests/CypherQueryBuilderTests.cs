using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Abstraction.Clauses;

namespace Cyanide.Cypher.Tests;

public class CypherQueryBuilderTests
{
    private readonly IQuery _queryBuilder = Factory.QueryBuilder();

    #region MATCH

    [Fact]
    public void Translate_With_MATCH_DirectRelation_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.InDirect)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.BiDirect)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.UnDirect)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN")
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", alias: "x")
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (p:Person)-[x:LIVES_IN]-(c:City) RETURN p.name, c.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_With_MATCH_AndFieldAndAlias_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "a", [new Field("name", "'Martin Sheen'")]))
            )
            .Return(q =>
                q.WithField("name", "a")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (a:Person {name: 'Martin Sheen'}) RETURN a.name";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_With_MATCH_AndFieldWithoutAlias_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Movie", "", [new Field("title", "'Wall Street'")]))
                    .WithRelationship("ACTED_IN|DIRECTED", RelationshipType.InDirect)
                    .WithNode(new Entity("Person", "person"))
            )
            .Return(q =>
                q.WithField("name", "person", "person")
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
                q.WithNode(new Entity("Person", "a",[new Field("name","'Martin Sheen'")]))
            )
            .Match(q =>
                q.WithNode(new Entity("a"))
                    .WithRelationship("DIRECTED", RelationshipType.Direct, "r")
                    .WithEmptyNode()
            )
            .Return(q =>
                q.WithField("name", "a").WithField("r")
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
                q.WithNode(new Entity("Person", "a", [new Field("name", "'Martin Sheen'")]))
            )
            .OptionalMatch(q =>
                q.WithNode(new Entity("a"))
                    .WithRelationship("DIRECTED", RelationshipType.Direct, "r")
                    .WithEmptyNode()
            )
            .Return(q =>
                q.WithField("name", "a").WithField("r")
            )
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (a:Person {name: 'Martin Sheen'}) OPTIONAL MATCH (a)-[r:DIRECTED]->() RETURN a.name, r";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    
    [Fact(Skip = "Not ready")]
    public void Translate_With_OPTIONAL_MATCH_InvalidOrderClause_ReturnsException()
    {
        // Act
        Assert.Throws<InvalidOperationException>(() =>
        {
            _queryBuilder
                .OptionalMatch(q =>
                    q.WithNode(new Entity("Person", "a", [new Field("name", "'Martin Sheen'")]))
                )
                .Return(q =>
                    q.WithField("name", "a").WithField("r")
                )
                .Build();
        });
    }

    #endregion

    #region WHERE

    [Fact]
    public void Translate_With_WHERE_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30"))
            .Return(q =>
                q
                    .WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").And(q1 => q1.Query("b.city=\"New York\"")))
            .Return(q =>
                q
                    .WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").Or(q1 => q1.Query("b.city=\"New York\"")))
            .Return(q =>
                q
                    .WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").Xor(q1 => q1.Query("b.city=\"New York\"")))
            .Return(q =>
                q
                    .WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").Not(q1 => q1.Query("b.city=\"New York\"")))
            .Return(q =>
                q
                    .WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").And(q1 => q1.IsNotNull("b.city")))
            .Return(q =>
                q
                    .WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "p"))
                    .WithRelationship("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30").And(q1 => q1.IsNull("b.city")))
            .Return(q =>
                q
                    .WithField("name", "p")
                    .WithField("name", "c")
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
                q.WithNode(new Entity("Person", "charlie",[new Field("name","'Charlie Sheen'")]))
            )
            .Create(q => q.WithNode(new Entity("Actor", "charlie")))
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
                q.WithNode(new Entity("Person", "person"))
            )
            .Where(q => q.IsNotNull("person.name"))
            .Create(q => q.WithNode(new Entity("Person", "anotherPerson", [new Field("name", "person.name")])))
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
                q.WithNode(new Entity("Person", "n",
                    [new Field("name", "'Andy'"), 
                        new Field("title", "'Developer'")])))
            .Build();

        // Assert
        var expectedQuery =
            "CREATE (n:Person {name: 'Andy', title: 'Developer'})";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_With_CREATE_WithoutAlias_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithNode(new Entity("Person", null,
                [new Field("name", "'Andy'"), 
                    new Field("title", "'Developer'")])))
            .Build();

        // Assert
        var expectedQuery =
            "CREATE (Person {name: 'Andy', title: 'Developer'})";
        Assert.Equal(resultQuery, expectedQuery);
    }

    [Fact]
    public void Translate_With_CREATE_FullPath_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithRelationship(new Entity("WORKS_AT", ""), RelationshipType.Direct, 
                        new Entity("Person", "andy", [new Field("name", "'Andy'")]), 
                        new Entity("neo"))
                    .WithRelationship("WORKS_AT", RelationshipType.InDirect)
                    .WithNode(new Entity("Person", "michael", [new Field("name", "'Michael'")]))
            )
            .Return(q => q.WithField("andy").WithField("michael"))                              
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
                q.WithNode(new Entity("Person", "keanu", [new Field("name", "'Keanu Reever'")]))
                    .WithNode(new Entity("Person", "laurence", [new Field("name", "'Laurence Fishburne'")]))
                    .WithRelationship(new Entity("ACTED_IN",""), RelationshipType.Direct, new Entity("keanu"),
                        new Entity("theMatrix"))
                    .WithRelationship(new Entity("ACTED_IN",""), RelationshipType.Direct, new Entity("laurence"),
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
                q.WithRelationship(new Entity("ACTED_IN", "r"), RelationshipType.Direct, 
                        new Entity("Person", "n", [new Field("name", "'Laurence Fishburne'")]), 
                        new Entity(string.Empty))
            )
            .Delete(q => q.WithNode("r"))
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
                q.WithNode(new Entity("Person", "n", [new Field("name", "'Carrie-Anne Moss'")]))
            )
            .DetachDelete(q => q.WithNode("n"))
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
                q.WithNode(new Entity("n", ""))
            )
            .Return(q => q.WithField("name", "n").WithField("age", "n"))
            .OrderBy(q => q.WithField("name", "n").WithField("age", "n"))
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
                q.WithNode(new Entity("n", ""))
            )
            .Return(q => q.WithField("name", "n").WithField("age", "n"))
            .OrderBy(b => b.WithField("name", "n").Descending())
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (n) RETURN n.name, n.age ORDER BY n.name DESC";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    #endregion
    
    #region SELECT
    
    [Fact]
    public void Translate_With_SELECT_RETURN_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithRelationship(
                    new Entity("r"), 
                    RelationshipType.Direct,
                    new Entity("Person", "", [new Field("name", "'Oliver Stone'")]), 
                    new Entity("movie"))
            )
            .Return(q => q.WithRelation("r"))
            .Build();

        // Assert
        var expectedQuery =
            "MATCH (:Person {name: 'Oliver Stone'})-[r]->(movie) RETURN type(r)";
        Assert.Equal(resultQuery, expectedQuery);
    }
    
    #endregion
}