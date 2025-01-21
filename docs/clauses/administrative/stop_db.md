### `STOP DATABASE`
```csharp
var resultQuery = _adminQueryBuilder
    .Stop(q => q.WithDatabase("db"))
    .Build();
```
**Output:**
```cypher
STOP DATABASE db
```
------