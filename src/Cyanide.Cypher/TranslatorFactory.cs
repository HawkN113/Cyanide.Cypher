using Cyanide.Cypher.Translators;
using Cyanide.Cypher.Translators.Abstraction;

namespace Cyanide.Cypher;
public sealed class TranslatorFactory
{
    public static IQueryTranslator CreateTranslator(TranslatorType type)
    {
        return type switch
        {
            TranslatorType.SQLToCypher => new SqlToCypherTranslator(),
            TranslatorType.CypherToSQL => new CypherToSqlTranslator(),
            _ => throw new ArgumentException("Unknown translator type"),
        };
    }
}