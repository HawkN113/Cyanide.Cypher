namespace Cyanide.Cypher.Builders.Abstraction.Common;

public interface IRelationship<out T> where T : class
{
    T WithRelationship(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "");

    T WithRelationship(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null);
}