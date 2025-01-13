using System.Text;

namespace Cyanide.Cypher.Builders.Admin.Commands;

public sealed class CreateDbQuery(StringBuilder createDbClauses) : ICreateAdmQueryDatabase, INotExistsDatabase
{
    private readonly List<string> _patterns = [];
    private bool _shouldReplaced;
    private bool _ifNotExists;
    
    public INotExistsDatabase WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
        return this;
    }
    
    public IReplaceDatabase IfNotExists()
    {
        _ifNotExists = true;
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

        if (_shouldReplaced && _ifNotExists)
            throw new InvalidOperationException("Either CREATE DATABASE [IF NOT EXISTS] or CREATE OR REPLACE DATABASE");
        createDbClauses.Append(!_shouldReplaced ? "CREATE " : "CREATE OR REPLACE ");
        createDbClauses.Append(string.Join(" ", _patterns));
    }
}