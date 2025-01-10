namespace Cyanide.Cypher.Builders.Abstraction;

public interface INode<out T> where T : class
{
    T WithEmptyNode();
    T WithNode(Entity entity);
}