### `OPTIONAL MATCH`
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "a", [new Field("name", "'Martin Sheen'")]))
    )
    .OptionalMatch(q =>
        q.WithNode(new Entity("a"))
            .WithRelation("DIRECTED", RelationshipType.Direct, "r")
            .WithEmptyNode()
    )
    .Return(q =>
        q.WithField("name", "a").WithField("r")
    )
    .Build();
```
**Output:**
```cypher
MATCH (a:Person {name: 'Martin Sheen'}) OPTIONAL MATCH (a)-[r:DIRECTED]->() RETURN a.name, r
```
------