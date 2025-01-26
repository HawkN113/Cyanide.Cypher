### `REMOVE`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("a", null, [new Field("name", "'Andy'")])))
    .Remove(q => q.WithField("age", "a"))
    .Return(q => q.WithField("name", "a").WithField("age", "a"))
    .Build();
```
**Output:**
```cypher
MATCH (a {name: 'Andy'}) REMOVE a.age RETURN a.name, a.age
```
------