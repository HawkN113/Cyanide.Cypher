using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin.Commands;

public sealed class UpdateDbQuery :
    IBuilderInitializer,
    IUpdateAdmQueryDatabase,
    ISetAccess
{
    private readonly List<string> _patterns = [];
    private StringBuilder _updateDbClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _updateDbClauses = clauseBuilder;
    }
    
    public ISetAccess WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
        return this;
    }

    public void SetAccessReadOnly()
    {
        _patterns.Add("SET ACCESS READ ONLY");
    }

    public void SetAccessReadWrite()
    {
        _patterns.Add("SET ACCESS READ WRITE");
    }

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_updateDbClauses.Length > 0)
        {
            _updateDbClauses.Append(' ');
        }
        _updateDbClauses.Append("ALTER ");
        _updateDbClauses.Append(string.Join(" ", _patterns));
    }
}