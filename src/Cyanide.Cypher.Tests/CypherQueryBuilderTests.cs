﻿using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Queries.General;

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
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (p:Person)-[:LIVES_IN]->(c:City) RETURN p.name, c.name", resultQuery);
    }

    [Fact]
    public void Translate_With_MATCH_InDirectRelation_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.InDirect)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (p:Person)<-[:LIVES_IN]-(c:City) RETURN p.name, c.name", resultQuery);
    }

    [Fact]
    public void Translate_With_MATCH_BiDirectRelation_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.BiDirect)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (p:Person)->[:LIVES_IN]<-(c:City) RETURN p.name, c.name", resultQuery);
    }

    [Fact]
    public void Translate_With_MATCH_UnDirectRelation_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.UnDirect)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (p:Person)<-[:LIVES_IN]->(c:City) RETURN p.name, c.name", resultQuery);
    }

    [Fact]
    public void Translate_With_MATCH_NonDirectRelation_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN")
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (p:Person)-[:LIVES_IN]-(c:City) RETURN p.name, c.name", resultQuery);
    }

    [Fact]
    public void Translate_With_MATCH_NonDirectRelationWithAlias_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", alias: "x")
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (p:Person)-[x:LIVES_IN]-(c:City) RETURN p.name, c.name", resultQuery);
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
        Assert.Equal("MATCH (a:Person {name: 'Martin Sheen'}) RETURN a.name", resultQuery);
    }

    [Fact]
    public void Translate_With_MATCH_AndFieldWithoutAlias_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Movie", "", [new Field("title", "'Wall Street'")]))
                    .WithRelation("ACTED_IN|DIRECTED", RelationshipType.InDirect)
                    .WithNode(new Entity("Person", "person"))
            )
            .Return(q =>
                q.WithField("name", "person", "person")
            )
            .Build();

        // Assert
        Assert.Equal(
            "MATCH (:Movie {title: 'Wall Street'})<-[:ACTED_IN|DIRECTED]-(person:Person) RETURN person.name AS person",
            resultQuery);
    }

    [Fact]
    public void Translate_With_MATCH_WithThreeNodes_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "a", [new Field("name", "'Martin Sheen'")]))
            )
            .Match(q =>
                q.WithNode(new Entity("a"))
                    .WithRelation("DIRECTED", RelationshipType.Direct, "r")
                    .WithEmptyNode()
            )
            .Return(q =>
                q.WithField("name", "a").WithField("r")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (a:Person {name: 'Martin Sheen'}) MATCH (a)-[r:DIRECTED]->() RETURN a.name, r",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_MATCH_DirectedRelation_WithTwoNodes_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithRelation(
                    new Entity("Person", "", [new Field("name", "'Oliver Stone'")]),
                    new Entity("movie"),
                    BasicRelationshipType.Directed)
            )
            .Return(q =>
                q.WithField("title", "movie")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (:Person {name: 'Oliver Stone'})-->(movie) RETURN movie.title",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_MATCH_InDirectRelation_WithTwoNodes_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithRelation(
                    new Entity("Person", "", [new Field("name", "'Oliver Stone'")]),
                    new Entity("movie"),
                    BasicRelationshipType.InDirected)
            )
            .Return(q =>
                q.WithField("title", "movie")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (:Person {name: 'Oliver Stone'})<--(movie) RETURN movie.title",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_MATCH_RelatedToRelation_WithTwoNodes_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithRelation(
                    new Entity("Person", "", [new Field("name", "'Oliver Stone'")]),
                    new Entity("movie"))
            )
            .Return(q =>
                q.WithField("title", "movie")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (:Person {name: 'Oliver Stone'})--(movie) RETURN movie.title",
            resultQuery);
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
                    .WithRelation("DIRECTED", RelationshipType.Direct, "r")
                    .WithEmptyNode()
            )
            .Return(q =>
                q.WithField("name", "a").WithField("r")
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (a:Person {name: 'Martin Sheen'}) OPTIONAL MATCH (a)-[r:DIRECTED]->() RETURN a.name, r",
            resultQuery);
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
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
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
        Assert.Equal("MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 RETURN p.name, c.name", resultQuery);
    }

    [Fact]
    public void Translate_With_WHERE_AND_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
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
        Assert.Equal(
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city=\"New York\" RETURN p.name, c.name",
            resultQuery);
    }

    [Fact]
    public void Translate_With_WHERE_OR_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
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
        Assert.Equal(
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 OR b.city=\"New York\" RETURN p.name, c.name",
            resultQuery);
    }

    [Fact]
    public void Translate_With_WHERE_XOR_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
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
        Assert.Equal(
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 XOR b.city=\"New York\" RETURN p.name, c.name",
            resultQuery);
    }

    [Fact]
    public void Translate_With_WHERE_NOT_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
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
        Assert.Equal(
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 NOT b.city=\"New York\" RETURN p.name, c.name",
            resultQuery);
    }

    [Fact]
    public void Translate_With_WHERE_IS_NOT_NULL_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
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
        Assert.Equal(
            "MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city IS NOT NULL RETURN p.name, c.name",
            resultQuery);
    }

    [Fact]
    public void Translate_With_WHERE_IS_NULL_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "p"))
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
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
        Assert.Equal("MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city IS NULL RETURN p.name, c.name",
            resultQuery);
    }

    #endregion

    #region CREATE

    [Fact]
    public void Translate_With_CREATE_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "charlie", [new Field("name", "'Charlie Sheen'")]))
            )
            .Create(q => q.WithNode(new Entity("Actor", "charlie")))
            .Build();

        // Assert
        Assert.Equal("MATCH (charlie:Person {name: 'Charlie Sheen'}) CREATE (charlie:Actor)", resultQuery);
    }

    [Fact]
    public void Translate_With_CREATE_WithRelations_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Person", "a"))
                    .WithNode(new Entity("Person", "b"))
            )
            .Where(q => q.Query("a.name = 'A'").And(q => q.Query("b.name = 'B'")))
            .Create(q => q.WithRelation(new Entity("RELTYPE", "r", [new Field("name", "a.name + '<->' + b.name")]),
                RelationshipType.Direct, new Entity("a"), new Entity("b")))
            .Return(q => q.WithType("r").WithField("name", "r"))
            .Build();

        // Assert
        Assert.Equal(
            "MATCH (a:Person), (b:Person) WHERE a.name = 'A' AND b.name = 'B' CREATE (a)-[r:RELTYPE {name: a.name + '<->' + b.name}]->(b) RETURN type(r), r.name",
            resultQuery);
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
        Assert.Equal(
            "MATCH (person:Person) WHERE person.name IS NOT NULL CREATE (anotherPerson:Person {name: person.name})",
            resultQuery);
    }

    [Fact]
    public void Translate_With_CREATE_MultiProperties_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithNode(new Entity("Person", "n",
                [
                    new Field("name", "'Andy'"),
                    new Field("title", "'Developer'")
                ])))
            .Build();

        // Assert
        Assert.Equal("CREATE (n:Person {name: 'Andy', title: 'Developer'})", resultQuery);
    }

    [Fact]
    public void Translate_With_CREATE_WithoutAlias_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithNode(new Entity("Person", null,
                [
                    new Field("name", "'Andy'"),
                    new Field("title", "'Developer'")
                ])))
            .Build();

        // Assert
        Assert.Equal("CREATE (Person {name: 'Andy', title: 'Developer'})", resultQuery);
    }

    [Fact]
    public void Translate_With_CREATE_FullPath_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithRelation(new Entity("WORKS_AT", ""), RelationshipType.Direct,
                        new Entity("Person", "andy", [new Field("name", "'Andy'")]),
                        new Entity("neo"))
                    .WithRelation("WORKS_AT", RelationshipType.InDirect)
                    .WithNode(new Entity("Person", "michael", [new Field("name", "'Michael'")]))
            )
            .Return(q => q.WithField("andy").WithField("michael"))
            .Build();

        // Assert
        Assert.Equal(
            "CREATE (andy:Person {name: 'Andy'})-[:WORKS_AT]->(neo)<-[:WORKS_AT]-(michael:Person {name: 'Michael'}) RETURN andy, michael",
            resultQuery);
    }

    [Fact]
    public void Translate_With_CREATE_MultiNodes_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithNode(new Entity("Person", "keanu", [new Field("name", "'Keanu Reever'")]))
                    .WithNode(new Entity("Person", "laurence", [new Field("name", "'Laurence Fishburne'")]))
                    .WithRelation(new Entity("ACTED_IN", ""), RelationshipType.Direct, new Entity("keanu"),
                        new Entity("theMatrix"))
                    .WithRelation(new Entity("ACTED_IN", ""), RelationshipType.Direct, new Entity("laurence"),
                        new Entity("theMatrix"))
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE (keanu:Person {name: 'Keanu Reever'}), (laurence:Person {name: 'Laurence Fishburne'}), (keanu)-[:ACTED_IN]->(theMatrix), (laurence)-[:ACTED_IN]->(theMatrix)",
            resultQuery);
    }

    #endregion

    #region DELETE

    [Fact]
    public void Translate_With_DELETE_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithRelation(new Entity("ACTED_IN", "r"), RelationshipType.Direct,
                    new Entity("Person", "n", [new Field("name", "'Laurence Fishburne'")]),
                    new Entity(string.Empty))
            )
            .Delete(q => q.WithNode("r"))
            .Build();

        // Assert
        Assert.Equal("MATCH (n:Person {name: 'Laurence Fishburne'})-[r:ACTED_IN]->() DELETE r", resultQuery);
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
        Assert.Equal("MATCH (n:Person {name: 'Carrie-Anne Moss'}) DETACH DELETE n", resultQuery);
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
        Assert.Equal("MATCH (n) RETURN n.name, n.age ORDER BY n.name, n.age", resultQuery);
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
        Assert.Equal("MATCH (n) RETURN n.name, n.age ORDER BY n.name DESC", resultQuery);
    }

    #endregion

    #region SELECT

    [Fact]
    public void Translate_With_SELECT_RETURN_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q =>
                q.WithRelation(
                    new Entity("r"),
                    RelationshipType.Direct,
                    new Entity("Person", "", [new Field("name", "'Oliver Stone'")]),
                    new Entity("movie"))
            )
            .Return(q => q.WithType("r"))
            .Build();

        // Assert
        Assert.Equal("MATCH (:Person {name: 'Oliver Stone'})-[r]->(movie) RETURN type(r)", resultQuery);
    }

    #endregion

    #region REMOVE

    [Fact]
    public void Translate_With_REMOVE_Query_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("a", null, [new Field("name", "'Andy'")])))
            .Remove(q => q.WithField("age", "a"))
            .Return(q => q.WithField("name", "a").WithField("age", "a"))
            .Build();

        // Assert
        Assert.Equal("MATCH (a {name: 'Andy'}) REMOVE a.age RETURN a.name, a.age", resultQuery);
    }

    #endregion

    #region SET

    [Fact]
    public void Translate_With_SET_WithProperties_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("n", null, [new Field("name", "'Andy'")])))
            .Set(q => q.WithField("surname", "n", "'Taylor'"))
            .Return(q => q.WithField("name", "n").WithField("surname", "n"))
            .Build();

        // Assert
        Assert.Equal("MATCH (n {name: 'Andy'}) SET n.surname = 'Taylor' RETURN n.name, n.surname", resultQuery);
    }

    [Fact]
    public void Translate_With_SET_WithMultiProperties_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("n", null, [new Field("name", "'Andy'")])))
            .Set(q => q.WithField("position", "n", "'Developer'").WithField("surname", "n", "'Taylor'"))
            .Build();

        // Assert
        Assert.Equal("MATCH (n {name: 'Andy'}) SET n.position = 'Developer', n.surname = 'Taylor'", resultQuery);
    }

    [Fact]
    public void Translate_With_SET_WithCondition_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("p", null, [new Field("name", "'Peter'")])))
            .Set(q => q.WithQuery("p += {}"))
            .Return(q => q.WithField("name", "p").WithField("age", "p"))
            .Build();

        // Assert
        Assert.Equal("MATCH (p {name: 'Peter'}) SET p += {} RETURN p.name, p.age", resultQuery);
    }

    #endregion

    #region LIMIT

    [Fact]
    public void Translate_With_LIMIT_WithTextCondition_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("n")))
            .Return(q => q.WithField("name", "n"))
            .OrderBy(q => q.WithField("name", "n"))
            .Limit(q => q.WithCount("1 + toInteger(3 * rand())"))
            .Build();

        // Assert
        Assert.Equal("MATCH (n) RETURN n.name ORDER BY n.name LIMIT 1 + toInteger(3 * rand())", resultQuery);
    }

    [Fact]
    public void Translate_With_LIMIT_WithPositiveNumber_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("n")))
            .Return(q => q.WithField("name", "n"))
            .OrderBy(q => q.WithField("name", "n"))
            .Limit(q => q.WithCount(3))
            .Build();

        // Assert
        Assert.Equal("MATCH (n) RETURN n.name ORDER BY n.name LIMIT 3", resultQuery);
    }

    #endregion

    #region SKIP

    [Fact]
    public void Translate_With_SKIP_WithTextCondition_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("n")))
            .Return(q => q.WithField("name", "n"))
            .OrderBy(q => q.WithField("name", "n"))
            .Skip(q => q.WithCount("1 + toInteger(3 * rand())"))
            .Build();

        // Assert
        Assert.Equal("MATCH (n) RETURN n.name ORDER BY n.name SKIP 1 + toInteger(3 * rand())", resultQuery);
    }

    [Fact]
    public void Translate_With_SKIP_WithPositiveNumber_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("n")))
            .Return(q => q.WithField("name", "n"))
            .OrderBy(q => q.WithField("name", "n"))
            .Skip(q => q.WithCount(1))
            .Limit(q => q.WithCount(3))
            .Build();

        // Assert
        Assert.Equal("MATCH (n) RETURN n.name ORDER BY n.name SKIP 1 LIMIT 3", resultQuery);
    }

    #endregion

    #region WITH

    [Fact]
    public void Translate_With_WITH_WithWildcard_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithRelation(new Entity("r"), RelationshipType.Direct, new Entity("person"),
                new Entity("otherPerson")))
            .With(q => q.WithField("*").WithType("r", "connectionType"))
            .Return(q => q.WithField("person.name").WithField("otherPerson.name").WithField("connectionType"))
            .Build();

        // Assert
        Assert.Equal(
            "MATCH (person)-[r]->(otherPerson) WITH *, type(r) AS connectionType RETURN person.name, otherPerson.name, connectionType",
            resultQuery);
    }

    [Fact]
    public void Translate_With_WITH_WithToUpper_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithRelation(new Entity("r"), RelationshipType.Direct, new Entity("person"),
                new Entity("otherPerson")))
            .With(q => q.ToUpper("name", "otherPerson", "upperCaseName").WithType("r", "connectionType"))
            .Return(q => q.WithField("person.name").WithField("otherPerson.name").WithField("connectionType"))
            .Build();

        // Assert
        Assert.Equal(
            "MATCH (person)-[r]->(otherPerson) WITH toUpper(otherPerson.name) AS upperCaseName, type(r) AS connectionType RETURN person.name, otherPerson.name, connectionType",
            resultQuery);
    }

    [Fact]
    public void Translate_With_WITH_WithCount_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithRelation(new Entity("r"), RelationshipType.Direct, new Entity("person"),
                new Entity("otherPerson")))
            .With(q => q.Count("name", "otherPerson", "upperCaseName").WithType("r", "connectionType"))
            .Return(q => q.WithField("person.name").WithField("otherPerson.name").WithField("connectionType"))
            .Build();

        // Assert
        Assert.Equal(
            "MATCH (person)-[r]->(otherPerson) WITH count(otherPerson.name) AS upperCaseName, type(r) AS connectionType RETURN person.name, otherPerson.name, connectionType",
            resultQuery);
    }

    #endregion

    #region UNION

    [Fact]
    public void Translate_With_UNION_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("Actor", "n")))
            .Return(q => q.WithField("name", "n", "name"))
            .Union(union => union
                .Match(q => q.WithNode(new Entity("Movie", "n")))
                .Return(q => q.WithField("title", "n", "name")))
            .Build();

        // Assert
        Assert.Equal("MATCH (n:Actor) RETURN n.name AS name UNION MATCH (n:Movie) RETURN n.title AS name",
            resultQuery);
    }

    [Fact]
    public void Translate_With_UNION_ALL_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Match(q => q.WithNode(new Entity("Actor", "n")))
            .Return(q => q.WithField("name", "n", "name"))
            .UnionAll(union => union
                .Match(q => q.WithNode(new Entity("Movie", "n")))
                .Return(q => q.WithField("title", "n", "name"))
            )
            .Build();

        // Assert
        Assert.Equal("MATCH (n:Actor) RETURN n.name AS name UNION ALL MATCH (n:Movie) RETURN n.title AS name",
            resultQuery);
    }

    #endregion
}