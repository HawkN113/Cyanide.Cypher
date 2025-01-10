namespace Cyanide.Cypher.Builders.Abstraction;

public interface IEmptyNode<out T> where T : class
{
    T WithEmptyNode();
}