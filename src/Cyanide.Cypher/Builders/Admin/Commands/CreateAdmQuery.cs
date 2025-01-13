using System.Text;

namespace Cyanide.Cypher.Builders.Admin.Commands;

public sealed class CreateAdmQuery(StringBuilder createAdmClauses) : ICreateAdmQueryDatabase, INotExistsDatabase
{
    private readonly List<string> _patterns = [];

    public INotExistsDatabase WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
        return this;
    }

    public void IfNotExists()
    {
        _patterns.Add("IF NOT EXISTS");
    }

    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (createAdmClauses.Length > 0)
        {
            createAdmClauses.Append(' ');
        }

        createAdmClauses.Append("CREATE ");
        createAdmClauses.Append(string.Join(" ", _patterns));
    }
}