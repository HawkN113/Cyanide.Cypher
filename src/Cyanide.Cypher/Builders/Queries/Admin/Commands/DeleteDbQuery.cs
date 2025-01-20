using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin.Commands;

public sealed class DeleteDbQuery :
    IBuilderInitializer,
    IStopAdmQueryDatabase
{
    private readonly List<string> _patterns = [];
    private StringBuilder _deleteDbClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _deleteDbClauses = clauseBuilder;
    }
    
    public void WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
    }

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_deleteDbClauses.Length > 0)
        {
            _deleteDbClauses.Append(' ');
        }
        _deleteDbClauses.Append("DROP ");
        _deleteDbClauses.Append(string.Join(" ", _patterns));
    }
}