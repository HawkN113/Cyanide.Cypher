### `UNION`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("Actor", "n")))
    .Return(q => q.WithField("name", "n", "name"))
    .Union(union => union
        .Match(q => q.WithNode(new Entity("Movie", "n")))
        .Return(q => q.WithField("title", "n", "name")))
    .Build();
```
**Output:**
```cypher
MATCH (n:Actor) RETURN n.name AS name UNION MATCH (n:Movie) RETURN n.title AS name
```
------

### `UNION ALL`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("Actor", "n")))
    .Return(q => q.WithField("name", "n", "name"))
    .UnionAll(union => union
        .Match(q => q.WithNode(new Entity("Movie", "n")))
        .Return(q => q.WithField("title", "n", "name"))
    )
    .Build();
```
**Output:**
```cypher
MATCH (n:Actor) RETURN n.name AS name UNION ALL MATCH (n:Movie) RETURN n.title AS name
```
------
