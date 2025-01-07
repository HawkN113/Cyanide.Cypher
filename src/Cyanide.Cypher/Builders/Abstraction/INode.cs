namespace Cyanide.Cypher.Builders.Abstraction;

public interface INode<out T> where T : class
{
    T EmptyNode();
    T Node(string type, string alias = "", Property[] properties = null);
    T Node(string type, Property property);
}