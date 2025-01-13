namespace Cyanide.Cypher.Builders.Abstraction.Common;

public interface IEmptyNode<out T> where T : class
{
    T WithEmptyNode();
}