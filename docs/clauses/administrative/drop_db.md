### `DROP DATABASE`
```csharp
var resultQuery = _adminQueryBuilder
    .Delete(q => q.WithDatabase("db"))
    .Build();
```
**Output:**
```cypher
DROP DATABASE db
```
------