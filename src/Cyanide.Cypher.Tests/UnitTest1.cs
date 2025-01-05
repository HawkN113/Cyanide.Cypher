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
}