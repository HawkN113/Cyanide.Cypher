using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin.Commands;

public sealed class StartDbQuery :
    IBuilderInitializer,
    IStartAdmQueryDatabase
{
    private readonly List<string> _patterns = [];
    private StringBuilder _startDbClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _startDbClauses = clauseBuilder;
    }
    
    public void WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
    }

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_startDbClauses.Length > 0)
        {
            _startDbClauses.Append(' ');
        }
        _startDbClauses.Append("START ");
        _startDbClauses.Append(string.Join(" ", _patterns));
    }
}