# Cyanide.Cypher

Cypher Query Builder is a lightweight and intuitive C# library designed to construct Cypher queries programmatically for use with Neo4j or other compatible graph databases. It simplifies query creation by providing a fluent and type-safe API, allowing developers to focus on query logic rather than string concatenation.

## Features

- **Fluent Query Builder**: Easily construct Cypher queries using a fluent API.
- **Customizable general clauses**: Support for the following Cypher clauses:
  - `MATCH`
  - `CREATE`
  - `RETURN`
  - `WHERE` (`IS NOT NULL`, `IS NULL`, `OR`, `XOR`)
  - `WITH`
  - `SKIP`
  - `LIMIT`
  - `SET`
  - `REMOVE`
  - `ORDER BY`
- **Customizable admin clauses**: Support for the following Cypher clauses:
  - `SHOW CURRENT USER`
  - `SHOW DATABASE(S)`
  - `CREATE USER`
  - `CREATE DATABASE`
  - `START DATABASE`
  - `STOP DATABASE`
  - `DROP DATABASE`
- **Extensibility**: Add custom functions or clauses based on your requirements.
- **Type Safety**: Benefit from strong typing to reduce runtime errors.

## Getting Started

### Installation

Add the library to your project via NuGet:
```bash
Install-Package Cyanide.Cypher --version 4.4.0
```

### Prerequisites

- .NET 8 or higher.
- A running instance of a Neo4j database (version `4.4` or `5.x`).

## Usage

### Create a general builder
```csharp
using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Queries.General;

IQuery _queryBuilder = Factory.QueryBuilder()
```

### Create an administrative builder
```csharp
using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Queries.Admin;

IAdminQuery _queryBuilder = Factory.AdminQueryBuilder();
```

### General clauses

#### `MATCH` (basic version)
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
#### `MATCH` (with bi-directional relation)
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
#### `MATCH` (with directional relation)
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
#### `MATCH` (with in-directional relation)
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
#### `MATCH` (with non-directional relation)
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
#### `CREATE`
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
#### `CREATE` (with multi nodes)
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
#### `WHERE`
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
#### `WHERE` (`IS NOT NULL`)
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
#### `WITH`
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
#### `SET`
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("p", null, [new Field("name", "'Peter'")])))
    .Set(q=>q.WithQuery("p += {}"))
    .Return(q => q.WithField("name", "p").WithField("age", "p"))
    .Build();
```
**Output:**
```cypher
MATCH (p {name: 'Peter'}) SET p += {} RETURN p.name, p.age
```
------
#### `SKIP`
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("n")))
    .Return(q => q.WithField("name", "n"))
    .OrderBy(q => q.WithField("name", "n"))
    .Skip(q=>q.WithCount(1))
    .Limit(q => q.WithCount(3))
    .Build();
```
**Output:**
```cypher
MATCH (n) RETURN n.name ORDER BY n.name SKIP 1 LIMIT 3
```
------

#### `LIMIT`
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("n")))
    .Return(q => q.WithField("name", "n"))
    .OrderBy(q => q.WithField("name", "n"))
    .Limit(q => q.WithCount("1 + toInteger(3 * rand())"))
    .Build();
```
**Output:**
```cypher
MATCH (n) RETURN n.name ORDER BY n.name LIMIT 1 + toInteger(3 * rand())
```
------

#### `REMOVE`
```csharp
var resultQuery = _queryBuilder
    .Match(q => q.WithNode(new Entity("a", null, [new Field("name", "'Andy'")])))
    .Remove(q => q.WithField("age", "a"))
    .Return(q => q.WithField("name", "a").WithField("age", "a"))
    .Build();
```
**Output:**
```cypher
MATCH (a {name: 'Andy'}) REMOVE a.age RETURN a.name, a.age
```
------

#### `ORDER BY`
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("n", ""))
    )
    .Return(q => q.WithField("name", "n").WithField("age", "n"))
    .OrderBy(q => q.WithField("name", "n").WithField("age", "n"))
    .Build();
```
**Output:**
```cypher
MATCH (n) RETURN n.name, n.age ORDER BY n.name, n.age
```
------

### Administrative clauses

#### `SKIP`
```csharp
```
**Output:**
```cypher
```
------

### Executing a Query
To execute the query, use your preferred `Neo4j driver` or client.