using Antlr4.Runtime;

namespace Cyanide.Cypher;

internal sealed class SyntaxErrorListener: BaseErrorListener
{
    private List<string> Errors { get; set; } = [];

    public override void SyntaxError(
        TextWriter output,
        IRecognizer recognizer,
        IToken offendingSymbol,
        int line,
        int charPositionInLine,
        string msg,
        RecognitionException e)
    {
        Errors.Add($"Line {line}:{charPositionInLine} - {msg}");
    }

    public int GetErrorCount()
    {
        return Errors.Count;
    }
}