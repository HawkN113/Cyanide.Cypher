### `SET`
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("p", null, [new Field("name", "'Peter'")])))
    .Set(q=>q.WithQuery("p += {}"))
    .Return(q => q.WithField("name", "p").WithField("age", "p"))
    .Build();
```
**Output:**
```cypher
MATCH (p {name: 'Peter'}) SET p += {} RETURN p.name, p.age
```
------