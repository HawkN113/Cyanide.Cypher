using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin.Commands;

public sealed class StopDbQuery :
    IBuilderInitializer,
    IStopAdmQueryDatabase
{
    private readonly List<string> _patterns = [];
    private StringBuilder _stopDbClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _stopDbClauses = clauseBuilder;
    }
    
    public void WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
    }

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_stopDbClauses.Length > 0)
        {
            _stopDbClauses.Append(' ');
        }
        _stopDbClauses.Append("STOP ");
        _stopDbClauses.Append(string.Join(" ", _patterns));
    }
}