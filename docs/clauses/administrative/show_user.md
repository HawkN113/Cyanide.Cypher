### `SHOW USERS`

**Sample**
```csharp
var resultQuery = _adminQueryBuilder
    .Show(q =>
        q.WithUsers()
            .WithAllFields()
            .WithCount()
    )
    .Build();
```
**Output:**
```cypher
SHOW USERS YIELD * RETURN count(*) AS count
```
------

### `SHOW CURRENT USER`

**Sample**
```csharp
var resultQuery = _adminQueryBuilder
    .Show(q =>
        q.AsCurrentUser()
            .WithAllFields()
            .WithCount()
    )
    .Build();
```
**Output:**
```cypher
SHOW CURRENT USER YIELD * RETURN count(*) AS count
```
------