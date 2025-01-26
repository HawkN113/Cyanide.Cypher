### `DETACH DELETE`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "n", [new Field("name", "'Carrie-Anne Moss'")]))
    )
    .DetachDelete(q => q.WithNode("n"))
    .Build();
```
**Output:**
```cypher
MATCH (n:Person {name: 'Carrie-Anne Moss'}) DETACH DELETE n
```
------