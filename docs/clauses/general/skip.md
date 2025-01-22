### `SKIP`
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("n")))
    .Return(q => q.WithField("name", "n"))
    .OrderBy(q => q.WithField("name", "n"))
    .Skip(q=>q.WithCount(1))
    .Limit(q => q.WithCount(3))
    .Build();
```
**Output:**
```cypher
MATCH (n) RETURN n.name ORDER BY n.name SKIP 1 LIMIT 3
```
------