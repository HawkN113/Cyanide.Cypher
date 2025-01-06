namespace Cyanide.Cypher.Builders.Abstraction;

public interface IRelationship<out T> where T : class
{
    T Relationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "");
}