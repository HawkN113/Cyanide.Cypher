using System.Text;

namespace Cyanide.Cypher.Builders.Admin.Commands;

public interface ICreateAdmQueryDatabase
{
    IfNotExistsDatabase WithDatabase(string databaseName);
}

public interface IfNotExistsDatabase
{
    IfNotExistsDatabase IfNotExists();
}

public sealed class CreateAdmQuery(StringBuilder createAdmClauses) : ICreateAdmQueryDatabase, IfNotExistsDatabase
{
    private readonly List<string> _patterns = [];

    public IfNotExistsDatabase WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
        return this;
    }

    public IfNotExistsDatabase IfNotExists()
    {
        _patterns.Add("IF NOT EXISTS");
        return this;
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