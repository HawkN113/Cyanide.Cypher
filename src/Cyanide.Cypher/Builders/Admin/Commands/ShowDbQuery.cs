using System.Text;

namespace Cyanide.Cypher.Builders.Admin.Commands;

public sealed class ShowDbQuery(StringBuilder showDbClauses) : IShowAdmQueryDatabase, IAllFieldsDatabase, IFieldsCountDatabase
{
    private readonly List<string> _patterns = [];

    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (showDbClauses.Length > 0)
        {
            showDbClauses.Append(' ');
        }

        showDbClauses.Append("SHOW ");
        showDbClauses.Append(string.Join(" ", _patterns));
    }

    public IAllFieldsDatabase WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
        return this;
    }

    public void AsDefault()
    {
        _patterns.Add("DEFAULT DATABASE");
    }

    public void AsHome()
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
}