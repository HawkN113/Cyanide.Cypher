namespace Cyanide.Cypher.Builders.Abstraction.Common;

public interface INode<out T> where T : class
{
    T WithNode(Entity entity);
}