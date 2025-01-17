using Cyanide.Cypher.Builders.Models;

namespace Cyanide.Cypher.Builders.Abstraction.Nodes;

public interface INode<out T> where T : class
{
    T WithNode(Entity entity);
}