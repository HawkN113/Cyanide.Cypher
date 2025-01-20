# cyanide.cypher

- Istall packages:
  - antlr4.runtime.standard
  - antlr4.runtime
- Run command: `dotnet tool install --global Antlr4CodeGenerator.Tool --version 2.3.0`
  https://github.com/tunnelvisionlabs/antlr4cs
- https://github.com/kaby76/Antlr4BuildTasks/issues/32
- https://github.com/kaby76/Antlr4BuildTasks
- https://github.com/mike-lischke/vscode-antlr4/blob/master/doc/extension-settings.md#grammar-formatting
- https://github.com/kaby76/Antlr4BuildTasks/blob/master/Antlr4BuildTasks/Antlr4BuildTasks.xml

Admin
- https://neo4j.com/docs/cypher-manual/4.4/administration/databases/#administration-databases-alter-database
- https://neo4j.com/docs/cypher-manual/4.4/administration/access-control/manage-users/#access-control-create-users (not completed)
- https://neo4j.com/docs/cypher-manual/4.4/administration/databases/#administration-databases-create-database (not completed)

- https://neo4j.com/docs/cypher-manual/4.4/clauses/with/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/skip/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/limit/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/set/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/remove/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/order-by/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/delete/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/create/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/return/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/match/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/optional-match/
- https://neo4j.com/docs/cypher-manual/4.4/clauses/where/ (not completed)

```
@startuml
hide empty description
[*] --> Match
Match --> Select
Match --> Match
Match --> OptionalMatch
Match --> Create
Match --> Delete
Match --> DetachDelete
Match --> Where

Select --> OrderBy
Select --> Build

OptionalMatch --> OptionalMatch
OptionalMatch --> Select
OptionalMatch --> Match
OptionalMatch --> Where
OptionalMatch --> Create
OptionalMatch --> Delete
OptionalMatch --> DetachDelete

Where --> Select

Create --> Select
Create --> Build

Delete --> Build
DetachDelete --> Build
OrderBy --> Build

Match: MATCH clause
OptionalMatch: OPTIONAL MATCH clause
Delete: DELETE clause
DetachDelete: DETACH DELETE clause
Where: WHERE subclause
OrderBy: ORDER BY subclause
Create: CREATE clause
Select: RETURN clause
Build: Cypher query

Build --> [*]
@enduml
```