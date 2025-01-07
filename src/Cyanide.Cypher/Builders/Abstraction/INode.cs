namespace Cyanide.Cypher.Builders.Abstraction;

public interface INode<out T> where T : class
{
    T EmptyNode();
    T Node(Entity entity);
}