namespace Cyanide.Cypher.Abstraction;

public interface IQueryTranslator
{
    string Translate(string inputQuery);
}