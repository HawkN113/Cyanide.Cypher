### `MATCH`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "a", [new Field("name", "'Martin Sheen'")]))
    )
    .Return(q =>
        q.WithField("name", "a")
    )
    .Build();
```
**Output:**
```cypher
MATCH (a:Person {name: 'Martin Sheen'}) RETURN a.name
```
------

### `MATCH` (with bi-directional relation)

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "p"))
            .WithRelation("LIVES_IN", RelationshipType.BiDirect)
            .WithNode(new Entity("City", "c"))
    )
    .Return(q =>
        q.WithField("name", "p")
            .WithField("name", "c")
    )
    .Build();
```
**Output:**
```cypher
MATCH (p:Person)->[:LIVES_IN]<-(c:City) RETURN p.name, c.name
```
------

### `MATCH` (with directional relation)

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "p"))
            .WithRelation("LIVES_IN", RelationshipType.Direct)
            .WithNode(new Entity("City", "c"))
    )
    .Return(q =>
        q.WithField("name", "p")
            .WithField("name", "c")
    )
    .Build();
```
**Output:**
```cypher
MATCH (p:Person)-[:LIVES_IN]->(c:City) RETURN p.name, c.name
```
------

### `MATCH`(with in-directional relation)

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "p"))
            .WithRelation("LIVES_IN", RelationshipType.InDirect)
            .WithNode(new Entity("City", "c"))
    )
    .Return(q =>
        q.WithField("name", "p")
            .WithField("name", "c")
    )
    .Build();
```
**Output:**
```cypher
MATCH (p:Person)<-[:LIVES_IN]-(c:City) RETURN p.name, c.name
```
------

### `MATCH` (with non-directional relation)

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "p"))
            .WithRelation("LIVES_IN")
            .WithNode(new Entity("City", "c"))
    )
    .Return(q =>
        q.WithField("name", "p")
            .WithField("name", "c")
    )
    .Build();
```
**Output:**
```cypher
MATCH (p:Person)-[:LIVES_IN]-(c:City) RETURN p.name, c.name
```
------