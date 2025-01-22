using System.Text;

namespace Cyanide.Cypher.Builders.Abstraction;

public interface IBuilderInitializer
{
    void Initialize(StringBuilder clauseBuilder);
    void End();
}