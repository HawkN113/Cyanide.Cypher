using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Admin.Abstraction.Clauses;

namespace Cyanide.Cypher.Builders.Admin.Commands;

public sealed class ShowDbQuery :
    IBuilderInitializer,
    IShowAdmQueryDatabase,
    IAllFieldsDatabase,
    IFieldsCountDatabase,
    IShowAllDatabases
{
    private readonly List<string> _patterns = [];
    private StringBuilder _showDbClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _showDbClauses = clauseBuilder;
    }

    public IAllFieldsDatabase WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
        return this;
    }

    public void AsDefaultDatabase()
    {
        _patterns.Add("DEFAULT DATABASE");
    }

    public void AsHomeDatabase()
    {
        _patterns.Add("HOME DATABASE");
    }

    public IFieldsCountDatabase WithAllFields()
    {
        _patterns.Add("YIELD *");
        return this;
    }

    public void WithCount()
    {
        _patterns.Add("RETURN count(*) AS count");
    }

    public void WithDatabases()
    {
        _patterns.Add("DATABASES");
    }

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_showDbClauses.Length > 0)
        {
            _showDbClauses.Append(' ');
        }

        _showDbClauses.Append("SHOW ");
        _showDbClauses.Append(string.Join(" ", _patterns));
    }
}