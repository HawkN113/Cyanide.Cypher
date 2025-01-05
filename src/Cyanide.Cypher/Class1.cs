using Antlr4.Runtime;

namespace Cyanide.Cypher;

public class Class1
{
    public void Test()
    {
        string input = "SELECT name, age FROM users";

        // Create an input stream from the query
        var inputStream = new AntlrInputStream(input);

        // Create a lexer instance
        var lexer = new SQLLexer(inputStream);

        // Create a token stream
        var tokenStream = new CommonTokenStream(lexer);

        // Create a parser instance
        var parser = new SQLParser(tokenStream);

        // Parse the input
        var queryContext = parser.query();

        // Print the parse tree
        Console.WriteLine(queryContext.ToStringTree(parser));
    }
}