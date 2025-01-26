### `STOP DATABASE`

**Sample**
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