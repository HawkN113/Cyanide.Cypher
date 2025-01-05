using Cyanide.Cypher.Translators.Abstraction;

namespace Cyanide.Cypher.Translators;

internal class CypherToSqlTranslator: IQueryTranslator
{
    public string Translate(string inputQuery)
    {
        return "SELECT * FROM Nodes";
    }
}