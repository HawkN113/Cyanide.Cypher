# Cyanide.Cypher

Cypher query builder is a lightweight and intuitive C# library designed to construct Cypher queries programmatically for use with Neo4j graph database. It simplifies query creation by providing a fluent and type-safe API, allowing developers to focus on query logic rather than string concatenation.

## Features

- **Fluent query builder**: Easily construct Cypher queries using a fluent API.
- **Customizable general clauses / subclauses**: Support for the following Cypher clauses:
    - `MATCH`,`OPTIONAL MATCH`
    - `CREATE`
    - `RETURN`
    - `WHERE` (`IS NOT NULL`,`IS NULL`, `OR`,`XOR`,`NOT`)
    - `WITH` (functions `toUpper`,`count`)
    - `SKIP`
    - `LIMIT`
    - `SET`
    - `SKIP`
    - `REMOVE`
    - `ORDER BY`
    - `DELETE`,`DETACH DELETE`
    - `UNION`,`UNION ALL`
- **Customizable administrative clauses**: Support for the following Cypher clauses (limited support):
    - `SHOW CURRENT USER`,`SHOW USERS`
    - `SHOW DATABASE`,`SHOW DATABASES`
    - `CREATE USER`,`CREATE OR REPLACE USER`
    - `CREATE DATABASE`
    - `START DATABASE`
    - `STOP DATABASE`
    - `ALTER DATABASE`
    - `DROP DATABASE`

More information about clauses is available an [official site](https://neo4j.com/docs/cypher-manual/4.4/clauses/)

### Prerequisites

- .NET 8 or higher.
- A running instance of a Neo4j database:
    - version `4.4`

## Usage

### Create a general builder
```csharp
using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Queries.General;

IQuery queryBuilder = Factory.QueryBuilder()
```

#### Sample
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

### Create an administrative builder
```csharp
using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Queries.Admin;

IAdminQuery adminQueryBuilder = Factory.AdminQueryBuilder();
```

#### Sample
```csharp
var resultQuery = _adminQueryBuilder
    .Show(q =>
        q.WithDatabases()
    )
    .Build();
```
**Output:**
```cypher
SHOW DATABASES
```
------