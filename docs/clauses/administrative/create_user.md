### `CREATE USER`
```csharp
var resultQuery = _adminQueryBuilder
    .Create(q =>
        q.WithUser("jake")
            .WithPassword("'abc'", PasswordType.Encrypted, true)
            .SetStatus(UserStatus.Suspended)
            .SetHomeDb("anotherDb")
    )
    .Build();
```
**Output:**
```cypher
CREATE USER jake SET ENCRYPTED PASSWORD 'abc' CHANGE REQUIRED SET STATUS SUSPENDED SET HOME DATABASE anotherDb
```
------

### `CREATE OR REPLACE USER`
```csharp
var resultQuery = _adminQueryBuilder
    .Create(q =>
        q.WithUser("jake")
            .WithPassword("'abc'")
            .SetStatus(UserStatus.Suspended)
            .SetHomeDb("anotherDb")
            .Replace()
    )
    .Build();
```
**Output:**
```cypher
CREATE OR REPLACE USER jake SET PLAINTEXT PASSWORD 'abc' CHANGE NOT REQUIRED SET STATUS SUSPENDED SET HOME DATABASE anotherDb
```
------