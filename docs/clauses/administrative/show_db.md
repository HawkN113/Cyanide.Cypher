
### `SHOW DATABASES`
```csharp
var resultQuery = _adminQueryBuilder
    .Show(q =>
        q.WithDatabases()
    )
    .Build();

**Output:**
```cypher
SHOW DATABASES
```
------

### `SHOW DATABASE`
```csharp
var resultQuery = _adminQueryBuilder
    .Show(q =>
        q.WithDatabase("db")
            .WithAllFields()
    )
    .Build();
```
**Output:**
```cypher
SHOW DATABASE db YIELD *
```
------