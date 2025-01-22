### `CREATE DATABASE`
```csharp
var resultQuery = _adminQueryBuilder
    .Create(q =>
        q.WithDatabase("db")
            .IfNotExists()
    )
    .Build();
```
**Output:**
```cypher
CREATE DATABASE db IF NOT EXISTS
```
------