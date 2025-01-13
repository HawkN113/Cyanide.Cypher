using System.Text;

namespace Cyanide.Cypher.Builders.Admin.Commands;

public sealed class CreateDbQuery(StringBuilder createDbClauses) : ICreateAdmQueryDatabase, INotExistsDatabase, IReplaceDatabase
{
    private readonly List<string> _patterns = [];
    private bool _shouldReplaced;
    
    public INotExistsDatabase WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
        return this;
    }
    
    public IReplaceDatabase IfNotExists()
    {
        _patterns.Add("IF NOT EXISTS");
        return this;
    }

    public void Replace()
    {
        _shouldReplaced = true;
    }
    

    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (createDbClauses.Length > 0)
        {
            createDbClauses.Append(' ');
        }
        createDbClauses.Append(!_shouldReplaced ? "CREATE " : "CREATE OR REPLACE ");
        createDbClauses.Append(string.Join(" ", _patterns));
    }
}