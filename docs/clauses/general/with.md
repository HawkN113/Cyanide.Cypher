### `WITH`

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithRelation(new Entity("r"), RelationshipType.Direct, new Entity("person"),
        new Entity("otherPerson")))
    .With(q => q.WithField("*").WithType("r", "connectionType"))
    .Return(q => q.WithField("person.name").WithField("otherPerson.name").WithField("connectionType"))
    .Build();
```
**Output:**
```cypher
MATCH (person)-[r]->(otherPerson) WITH *, type(r) AS connectionType RETURN person.name, otherPerson.name, connectionType
```
------

### `WITH` (with `toUpper` function)

**Sample**
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithRelation(new Entity("r"), RelationshipType.Direct, new Entity("person"),
        new Entity("otherPerson")))
    .With(q => q.ToUpper("name","otherPerson","upperCaseName").WithType("r", "connectionType"))
    .Return(q => q.WithField("person.name").WithField("otherPerson.name").WithField("connectionType"))
    .Build();
```
**Output:**
```cypher
MATCH (person)-[r]->(otherPerson) WITH toUpper(otherPerson.name) AS upperCaseName, type(r) AS connectionType RETURN person.name, otherPerson.name, connectionType
```
------