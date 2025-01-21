### `CREATE`
```csharp
var resultQuery = _queryBuilder
    .Create(q =>
        q.WithRelation(new Entity("WORKS_AT", ""), RelationshipType.Direct,
                new Entity("Person", "andy", [new Field("name", "'Andy'")]),
                new Entity("neo"))
            .WithRelation("WORKS_AT", RelationshipType.InDirect)
            .WithNode(new Entity("Person", "michael", [new Field("name", "'Michael'")]))
    )
    .Return(q => q.WithField("andy").WithField("michael"))
    .Build();
```
**Output:**
```cypher
CREATE (andy:Person {name: 'Andy'})-[:WORKS_AT]->(neo)<-[:WORKS_AT]-(michael:Person {name: 'Michael'}) RETURN andy, michael
```
------

### `CREATE` (with multi nodes)
```csharp
var resultQuery = _queryBuilder
    .Create(q =>
        q.WithNode(new Entity("Person", "keanu", [new Field("name", "'Keanu Reever'")]))
            .WithNode(new Entity("Person", "laurence", [new Field("name", "'Laurence Fishburne'")]))
            .WithRelation(new Entity("ACTED_IN", ""), RelationshipType.Direct, new Entity("keanu"),
                new Entity("theMatrix"))
            .WithRelation(new Entity("ACTED_IN", ""), RelationshipType.Direct, new Entity("laurence"),
                new Entity("theMatrix"))
    )
    .Build()
```
**Output:**
```cypher
CREATE (keanu:Person {name: 'Keanu Reever'}), (laurence:Person {name: 'Laurence Fishburne'}), (keanu)-[:ACTED_IN]->(theMatrix), (laurence)-[:ACTED_IN]->(theMatrix)"
```
------