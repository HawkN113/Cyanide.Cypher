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
        var cypherQuery = sqlToCypher.Translate(sqlQuery);
        Assert.Pass();
    }
    
    [Test]
    public void Test2()
    {
        var cypherToSql = TranslatorFactory.CreateTranslator(TranslatorType.CypherToSQL);
        var cypherQuery = "MATCH (a:Person)-[:FRIEND]->(b:Person)\nWHERE a.age > 30 AND b.city = \"New York\"\nRETURN a.name, b.name";
        var sqlQuery = cypherToSql.Translate(cypherQuery);
        Assert.Pass();
    }
}