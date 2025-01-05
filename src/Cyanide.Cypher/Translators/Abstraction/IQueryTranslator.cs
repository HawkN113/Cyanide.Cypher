namespace Cyanide.Cypher.Translators.Abstraction;

public interface IQueryTranslator
{
    string Translate(string inputQuery);
}