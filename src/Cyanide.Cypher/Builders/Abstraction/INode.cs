namespace Cyanide.Cypher.Builders.Abstraction;

public interface INode<out T> where T : class
{
    T WithNode(Entity entity);
}