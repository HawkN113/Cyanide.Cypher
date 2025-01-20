# Cyanide.Cypher

Cypher Query builder is a lightweight and intuitive C# library designed to construct Cypher queries programmatically for use with Neo4j or other compatible graph databases. It simplifies query creation by providing a fluent and type-safe API, allowing developers to focus on query logic rather than string concatenation.

## Features

- **Fluent Query builder**: Easily construct Cypher queries using a fluent API.
- **Customizable general clauses / subclauses**: Support for the following Cypher clauses:
  - `MATCH`,`OPTIONAL MATCH`
  - `CREATE`
  - `RETURN`
  - `WHERE` (`IS NOT NULL`,`IS NULL`, `OR`,`XOR`,`NOT`)
  - `WITH` (functions `toUpper`,`count`)
  - `SKIP`
  - `LIMIT`
  - `SET`
  - `REMOVE`
  - `ORDER BY`
  - `DELETE`,`DETACH DELETE`
- **Customizable administrative clauses**: Support for the following Cypher clauses (limited support):
  - `SHOW CURRENT USER`
  - `SHOW DATABASE`
  - `SHOW USERS`
  - `SHOW DATABASES`
  - `CREATE USER`,`CREATE OR REPLACE USER`
  - `CREATE DATABASE`
  - `START DATABASE`
  - `STOP DATABASE`
  - `ALTER DATABASE`
  - `DROP DATABASE`

More information about clauses is available an [official site](https://neo4j.com/docs/cypher-manual/4.4/clauses/)

## Getting Started

### Installation

Add the library to your project via NuGet:
```bash
Install-Package Cyanide.Cypher --version 4.4.0
```

### Prerequisites

- .NET 8 or higher.
- A running instance of a Neo4j database:
  - version `4.4` 
  - version `5.0`

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

IAdminQuery _adminQueryBuilder = Factory.AdminQueryBuilder();
```

#### [`MATCH`](#match) (basic version)
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

#### [`OPTIONAL MATCH`](#optional_match)
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "a", [new Field("name", "'Martin Sheen'")]))
    )
    .OptionalMatch(q =>
        q.WithNode(new Entity("a"))
            .WithRelation("DIRECTED", RelationshipType.Direct, "r")
            .WithEmptyNode()
    )
    .Return(q =>
        q.WithField("name", "a").WithField("r")
    )
    .Build();
```
**Output:**
```cypher
MATCH (a:Person {name: 'Martin Sheen'}) OPTIONAL MATCH (a)-[r:DIRECTED]->() RETURN a.name, r
```
------

#### [`MATCH`](#match_bi_directional) (with bi-directional relation)
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

#### [`MATCH`](#match_directional) (with directional relation)
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

#### [`MATCH`](#match_in_directional) (with in-directional relation)
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

#### [`MATCH`](#match_non_directional) (with non-directional relation)
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

#### [`CREATE`](#create)
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

#### [`CREATE`](#create_multi_nodes) (with multi nodes)
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

#### [`WHERE`](#where)
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

#### [`WHERE`](#where_not_null) (`IS NOT NULL`)
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

#### [`WITH`](#with)
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

#### [`WITH`](#with_to_upper) (with `toUpper` function)
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

#### [`SET`](#set)
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

#### [`SKIP`](#skip)
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

#### [`LIMIT`](#limit)
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

#### [`REMOVE`](#remove)
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

#### [`ORDER BY`](#order_by)
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

#### [`DELETE`](#delete)
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithRelation(new Entity("ACTED_IN", "r"), RelationshipType.Direct,
            new Entity("Person", "n", [new Field("name", "'Laurence Fishburne'")]),
            new Entity(string.Empty))
    )
    .Delete(q => q.WithNode("r"))
    .Build();
```
**Output:**
```cypher
MATCH (n:Person {name: 'Laurence Fishburne'})-[r:ACTED_IN]->() DELETE r
```
------

#### [`DETACH DELETE`](#detach_delete)
```csharp
var resultQuery = _queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Person", "n", [new Field("name", "'Carrie-Anne Moss'")]))
    )
    .DetachDelete(q => q.WithNode("n"))
    .Build();
```
**Output:**
```cypher
MATCH (n:Person {name: 'Carrie-Anne Moss'}) DETACH DELETE n
```
------

### Administrative clauses

#### [`SHOW DATABSES`](#show_dbs)
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

#### [`SHOW DATABASE`](#show_db)
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

#### [`SHOW USERS`](#show_users)
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

#### [`SHOW CURRENT USER`](#show_current_user)
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

#### [`CREATE USER`](#create_user)
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

#### [`CREATE OR REPLACE USER`](#create_or_replace_user)
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

#### [`CREATE DATABASE`](#create_db)
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

#### [`START DATABASE`](#start_db)
```csharp
var resultQuery = _adminQueryBuilder
    .Start(q => q.WithDatabase("db"))
    .Build();
```
**Output:**
```cypher
START DATABASE db
```
------

#### [`STOP DATABASE`](#stop_db)
```csharp
var resultQuery = _adminQueryBuilder
    .Stop(q => q.WithDatabase("db"))
    .Build();
```
**Output:**
```cypher
STOP DATABASE db
```
------

#### [`DROP DATABASE`](#drop_db)
```csharp
var resultQuery = _adminQueryBuilder
    .Delete(q => q.WithDatabase("db"))
    .Build();
```
**Output:**
```cypher
DROP DATABASE db
```
------

#### [`ALTER DATABASE`](#alter_db)
```csharp
var resultQuery = _adminQueryBuilder
    .Update(q => q.WithDatabase("db").SetAccessReadWrite())
    .Build();
```
**Output:**
```cypher
ALTER DATABASE db SET ACCESS READ WRITE
```
------

### Executing a query
To execute the query, use your preferred `Neo4j driver` or client.