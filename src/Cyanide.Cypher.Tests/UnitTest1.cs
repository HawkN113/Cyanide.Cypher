namespace Cyanide.Cypher.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var sqlToCypher = TranslatorFactory.CreateTranslator(TranslatorType.SQLToCypher);
        var sqlQuery = "SELECT name, id FROM Users WHERE age > 30 GROUP BY name HAVING COUNT(*) > 2";
        var resultQuery = sqlToCypher.Translate(sqlQuery);
        Assert.That(resultQuery,Is.EqualTo("MATCH (n:Users)\r\nWHERE age>30\r\nWITH name\r\nWHERE COUNT(*)>2\r\nRETURN name, n.id;\r\n"));
    }
    
    [Test]
    public void Test2()
    {
        var cypherToSql = TranslatorFactory.CreateTranslator(TranslatorType.CypherToSQL);
        var cypherQuery = "MATCH (a:Person)-[:FRIEND]->(b:Person)\nWHERE a.age > 30 AND b.city = \"New York\"\nRETURN a.name, b.name";
        var resultQuery = cypherToSql.Translate(cypherQuery);
        Assert.That(resultQuery, Is.EqualTo("SELECT a.name, b.name FROM Person AS a JOIN Person AS b ON a.FRIEND_id = b.id WHERE a.age>30 AND b.city=\"New York\""));
    }
}