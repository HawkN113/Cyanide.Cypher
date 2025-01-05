using Cyanide.Cypher.Translators.Abstraction;

namespace Cyanide.Cypher.Tests;

public class CypherToSqlTranslatorTests
{
    private IQueryTranslator _translator;

    [SetUp]
    public void SetUp()
    {
        _translator = TranslatorFactory.CreateTranslator(TranslatorType.CypherToSQL);
    }

    [Test]
    public void Translate_CypherQueryWithMatch_ReturnsCorrectSql()
    {
        // Arrange
        var cypherQuery = "MATCH (a:Person) RETURN a.name";

        // Act
        var sqlQuery = _translator.Translate(cypherQuery);

        // Assert
        var expectedSql = "SELECT a.name FROM Person AS a";
        Assert.That(sqlQuery, Is.EqualTo(expectedSql));
    }

    [Test]
    public void Translate_CypherQueryWithWhereClause_ReturnsCorrectSql()
    {
        // Arrange
        var cypherQuery = "MATCH (a:Person) WHERE a.age > 30 RETURN a.name";

        // Act
        var sqlQuery = _translator.Translate(cypherQuery);

        // Assert
        var expectedSql = "SELECT a.name FROM Person AS a WHERE a.age>30";
        Assert.That(sqlQuery, Is.EqualTo(expectedSql));
    }

    [Test]
    public void Translate_CypherQueryWithJoin1_ReturnsCorrectSql()
    {
        // Arrange
        var cypherQuery = "MATCH (a:Person)-[:KNOWS]->(b:Person) RETURN a.name, b.name";

        // Act
        var sqlQuery = _translator.Translate(cypherQuery);

        // Assert
        var expectedSql = "SELECT a.name, b.name FROM Person AS a INNER JOIN Person AS b ON a.id = b.id";
        Assert.That(sqlQuery, Is.EqualTo(expectedSql));
    }
    
    [Test]
    public void Translate_CypherQueryWithJoin2_ReturnsCorrectSql()
    {
        // Arrange
        var cypherQuery = "MATCH (p:Person)-[:LIVES_IN]->(c:City) RETURN p.name, p.age, c.name";

        // Act
        var sqlQuery = _translator.Translate(cypherQuery);

        // Assert
        var expectedSql = "SELECT p.name AS person_name, p.age AS person_age, c.name AS city_name FROM Person AS p INNER JOIN LivesIn ON p.id = LivesIn.person_id INNER JOIN City AS c ON LivesIn.city_id = c.id";
        Assert.That(sqlQuery, Is.EqualTo(expectedSql));
    }

    [Test]
    public void Translate_CypherQueryWithInvalidQuery_ThrowsException()
    {
        // Arrange
        var cypherQuery = "MATCH (a:Person) WHERE a.name RETURN";

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _translator.Translate(cypherQuery));
    }
}