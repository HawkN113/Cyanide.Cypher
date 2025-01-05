grammar SQL;

options {
    language = CSharp;
}

sqlStatement
    : selectStatement EOF
    ;

selectStatement
    : 'SELECT' selectElements
      'FROM' tableSource
      (joinClause)*
      (whereClause)?
      (groupByClause)?
      (havingClause)?
    ;

selectElements
    : '*'
    | columnName (',' columnName)*
    ;

tableSource
    : tableName
    ;

joinClause
    : joinType 'JOIN' tableName 'ON' condition
    ;

joinType
    : 'INNER'
    | 'LEFT'
    | 'RIGHT'
    | 'FULL'
    | /* Default is INNER if omitted */ 
    ;

whereClause
    : 'WHERE' condition
    ;

groupByClause
    : 'GROUP BY' columnName (',' columnName)*
    ;

havingClause
    : 'HAVING' condition
    ;

condition
    : expression (comparisonOperator expression)
    | '(' condition ')'
    | condition logicalOperator condition
    ;

expression
    : columnName
    | constant
    ;

comparisonOperator
    : '='
    | '<'
    | '>'
    | '<='
    | '>='
    | '<>'
    ;

logicalOperator
    : 'AND'
    | 'OR'
    ;

tableName
    : IDENTIFIER
    ;

columnName
    : IDENTIFIER
    ;

constant
    : STRING
    | NUMBER
    ;

IDENTIFIER
    : [a-zA-Z_][a-zA-Z0-9_]*
    ;

STRING
    : '\'' .*? '\''
    ;

NUMBER
    : [0-9]+ ('.' [0-9]+)?
    ;

WS
    : [ \t\r\n]+ -> skip
    ;

COMMENT
    : '--' ~[\r\n]* -> skip
    ;
