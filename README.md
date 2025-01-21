# Cyanide.Cypher
| ![Cypher](docs/img/cyanide.cypher.png) | Cypher query builder is a lightweight and intuitive C# library designed to construct Cypher queries programmatically for use with Neo4j or other compatible graph databases. It simplifies query creation by providing a fluent and type-safe API, allowing developers to focus on query logic rather than string concatenation. |
|----------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

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
  - `SHOW CURRENT USER`,`SHOW USERS`
  - `SHOW DATABASE`,`SHOW DATABASES`
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

IQuery queryBuilder = Factory.QueryBuilder()
```

### Create an administrative builder
```csharp
using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Queries.Admin;

IAdminQuery adminQueryBuilder = Factory.AdminQueryBuilder();
```

| General clauses                                                                                                                                  | Administrative clauses                                                                                                             |
|--------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------|
| [`MATCH`](docs\clauses\general\match.md), [`OPTIONAL MATCH`](docs\clauses\general\optional_match.md)                                             | [`SHOW DATABASE`](docs\clauses\administrative\show_db.md),[`SHOW DATABASES`](docs\clauses\administrative\show_db.md)               |
| [`CREATE`](docs\clauses\general\create.md)                                                                                                       | [`SHOW CURRENT USER`](docs\clauses\administrative\show_user.md),[`SHOW USERS`](docs\clauses\administrative\show_user.md)           |
| [`WHERE`](docs\clauses\general\where.md)                                                                                                         | [`CREATE USER`](docs\clauses\administrative\create_user.md),[`CREATE OR REPLACE USER`](docs\clauses\administrative\create_user.md) |
| [`WITH`](docs\clauses\general\with.md)                                                                                                           | [`CREATE DATABASE`](docs\clauses\administrative\create_db.md)                                                                      |
| [`SET`](docs\clauses\general\set.md)                                                                                                             | [`STOP DATABASE`](docs\clauses\administrative\stop_db.md)                                                                          |
| [`SKIP`](docs\clauses\general\skip.md)                                                                                                           | [`START DATABASE`](docs\clauses\administrative\start_db.md)                                                                        |
| [`LIMIT`](docs\clauses\general\limit.md)                                                                                                         | [`DROP DATABASE`](docs\clauses\administrative\drop_db.md)                                                                          |
| [`REMOVE`](docs\clauses\general\remove.md), [`DELETE`](docs\clauses\general\delete.md), [`DETACH DELETE`](docs\clauses\general\detach_delete.md) | [`ALTER DATABASE`](docs\clauses\administrative\alter_db.md)                                                                        |
| [`ORDER BY`](docs\clauses\general\order_by.md)                                                                                                   |                                                                                                                                    |

### Executing a query
To execute the query, use your preferred `Neo4j driver` or client.