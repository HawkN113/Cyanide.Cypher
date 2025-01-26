### `LIMIT`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("n")))
    .Return(q => q.WithField("name", "n"))
    .OrderBy(q => q.WithField("name", "n"))
    .Limit(q => q.WithCount("1 + toInteger(3 * rand())"))
    .Build();
```
**Output:**
```cypher
MATCH (n) RETURN n.name ORDER BY n.name LIMIT 1 + toInteger(3 * rand())
```
------