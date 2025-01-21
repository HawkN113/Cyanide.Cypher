### `ALTER DATABASE`
```csharp
var resultQuery = _adminQueryBuilder
    .Update(q => q.WithDatabase("db").SetAccessReadWrite())
    .Build();
```
**Output:**
```cypher
ALTER DATABASE db SET ACCESS READ WRITE
```
------