### `WHERE`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "p"))
            .WithRelation("LIVES_IN", RelationshipType.Direct)
            .WithNode(new Entity("City", "c"))
    )
    .Where(q => q.Query("p.age > 30").And(q1 => q1.Query("b.city=\"New York\"")))
    .Return(q =>
        q
            .WithField("name", "p")
            .WithField("name", "c")
    )
    .Build();
```
**Output:**
```cypher
MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city=\"New York\" RETURN p.name, c.name
```
------

### `WHERE` (`IS NOT NULL`)

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "p"))
            .WithRelation("LIVES_IN", RelationshipType.Direct)
            .WithNode(new Entity("City", "c"))
    )
    .Where(q => q.Query("p.age > 30").And(q1 => q1.IsNotNull("b.city")))
    .Return(q =>
        q
            .WithField("name", "p")
            .WithField("name", "c")
    )
    .Build();
```
**Output:**
```cypher
MATCH (p:Person)-[:LIVES_IN]->(c:City) WHERE p.age > 30 AND b.city IS NOT NULL RETURN p.name, c.name
```
------