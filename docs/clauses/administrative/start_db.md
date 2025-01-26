### `START DATABASE`

**Sample**
```csharp
var resultQuery = _adminQueryBuilder
    .Start(q => q.WithDatabase("db"))
    .Build();
```
**Output:**
```cypher
START DATABASE db
```
------