namespace Cyanide.Cypher.Builders.Abstraction.Nodes;

public interface IEmptyNode<out T> where T : class
{
    T WithEmptyNode();
}