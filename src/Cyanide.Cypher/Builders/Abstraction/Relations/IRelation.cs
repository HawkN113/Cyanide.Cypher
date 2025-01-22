namespace Cyanide.Cypher.Builders.Abstraction.Relations;

public interface IRelation<out T> where T : class
{
    T WithRelation(string type, RelationshipType relation = RelationshipType.NonDirect, string alias = "");

    T WithRelation(Entity entity, RelationshipType relation = RelationshipType.NonDirect, Entity? left = null,
        Entity? right = null);
}