### `DELETE`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithRelation(new Entity("ACTED_IN", "r"), RelationshipType.Direct,
            new Entity("Person", "n", [new Field("name", "'Laurence Fishburne'")]),
            new Entity(string.Empty))
    )
    .Delete(q => q.WithNode("r"))
    .Build();
```
**Output:**
```cypher
MATCH (n:Person {name: 'Laurence Fishburne'})-[r:ACTED_IN]->() DELETE r
```
------