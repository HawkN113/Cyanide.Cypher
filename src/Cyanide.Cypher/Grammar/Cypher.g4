grammar Cypher;

options {
    language = CSharp;
}

cypherQuery
    : matchClause whereClause? returnClause EOF
    ;

matchClause
    : 'MATCH' matchPattern (',' matchPattern)*
    ;

matchPattern
    : '(' alias ':' label ')'
      ('-[' relationshipAlias? ':' relationshipLabel ']->' '(' alias ':' label ')')?
    ;
    
returnClause
    : 'RETURN' returnItems
    ;
    
whereClause
    : 'WHERE' condition
    ;
    
condition
    : comparison (logicalOperator comparison)*
    ;

comparison
    : property comparisonOperator value
    ;

returnItems
    : returnItem (',' returnItem)*
    ;

groupByItems
    : groupByItem (',' groupByItem)*
    ;

returnItem
    : alias '.' property
    ;

groupByItem
    : alias '.' property
    ;

logicalOperator
    : 'AND'
    | 'OR'
    ;

comparisonOperator
    : '='
    | '<>'
    | '<'
    | '<='
    | '>'
    | '>='
    ;

property
    : IDENTIFIER
    ;

value
    : STRING
    | NUMBER
    ;

label
    : IDENTIFIER
    ;

relationshipLabel
    : IDENTIFIER
    ;

alias
    : IDENTIFIER
    ;

relationshipAlias
    : IDENTIFIER
    ;

STRING
    : '"' (~["\\] | '\\' .)* '"'
    ;

NUMBER
    : [0-9]+ ('.' [0-9]+)?
    ;

IDENTIFIER
    : [a-zA-Z_][a-zA-Z0-9_]*
    ;

WS
    : [ \t\r\n]+ -> skip
    ;
