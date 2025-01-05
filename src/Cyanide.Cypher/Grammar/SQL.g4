grammar SQL;

options {
    language = CSharp;
}

query        : select_clause from_clause (where_clause)? ';';
select_clause: 'SELECT' column_list ;
from_clause  : 'FROM' table_name ;
where_clause : 'WHERE' condition ;

column_list  : column (',' column)* ;
column       : IDENTIFIER ('.' IDENTIFIER)? ;
table_name   : IDENTIFIER ;
condition    : column '=' column ;

IDENTIFIER   : [a-zA-Z_][a-zA-Z0-9_]* ;
WS           : [ \t\r\n]+ -> skip ;