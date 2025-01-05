using Cyanide.Cypher.Translators.Abstraction;

namespace Cyanide.Cypher.Tests;

public class SqlToCypherTranslatorTests
{
    private IQueryTranslator _translator;

    [SetUp]
    public void SetUp()
    {
        _translator = TranslatorFactory.CreateTranslator(TranslatorType.SQLToCypher);
    }

   [Test]
    public void Translate_SelectWithJoin_ReturnsCorrectCypherQuery()
    {
        // Arrange
        string sqlQuery = @"
            SELECT p.name, p.age, c.name 
            FROM Person p 
            JOIN LivesIn l ON p.id = l.person_id 
            JOIN City c ON l.city_id = c.id";

        string expectedCypherQuery = @"
            MATCH (p:Person)-[:LIVES_IN]->(c:City) 
            RETURN p.name, p.age, c.name";

        // Act
        string cypherQuery = _translator.Translate(sqlQuery);

        // Assert
        Assert.AreEqual(expectedCypherQuery.Trim(), cypherQuery.Trim());
    }

    [Test]
    public void Translate_SelectWithoutJoin_ReturnsCorrectCypherQuery()
    {
        // Arrange
        string sqlQuery = "SELECT name, age FROM Person";
        string expectedCypherQuery = "MATCH (p:Person) RETURN p.name, p.age";

        // Act
        string cypherQuery = _translator.Translate(sqlQuery);

        // Assert
        Assert.AreEqual(expectedCypherQuery, cypherQuery);
    }

    [Test]
    public void Translate_InvalidSql_ThrowsException()
    {
        // Arrange
        string invalidSqlQuery = "INVALID SQL";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _translator.Translate(invalidSqlQuery));
    }

    [Test]
    public void Translate_SelectWithWhereClause_ReturnsCorrectCypherQuery()
    {
        // Arrange
        var sqlQuery = @"SELECT p.name, p.age FROM Person AS p WHERE p.age > 30";

        var expectedCypherQuery = @"MATCH (p:Person)\nWHERE p.age > 30\nRETURN p.name, p.age";

        // Act
        var cypherQuery = _translator.Translate(sqlQuery);

        // Assert
        Assert.That(expectedCypherQuery, Is.EqualTo(cypherQuery));
    }

    [Test]
    public void Translate_SelectWithAggregateFunction_ReturnsCorrectCypherQuery()
    {
        // Arrange
        string sqlQuery = @"
            SELECT COUNT(*) AS total 
            FROM Person";

        string expectedCypherQuery = @"
            MATCH (p:Person) 
            RETURN COUNT(p) AS total";

        // Act
        string cypherQuery = _translator.Translate(sqlQuery);

        // Assert
        Assert.AreEqual(expectedCypherQuery.Trim(), cypherQuery.Trim());
    }
}