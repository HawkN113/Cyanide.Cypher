using System.Text;

namespace Cyanide.Cypher.Builders.Admin.Commands;

public sealed class ShowUserQuery(StringBuilder showUserlauses)
    : IAllFieldsUser, IShowAllUsers, IFieldsCountUser, ICurrentUser
{
    private readonly List<string> _patterns = [];

    public IFieldsCountUser WithAllFields()
    {
        _patterns.Add("YIELD *");
        return this;
    }

    public IAllFieldsUser WithUsers()
    {
        _patterns.Add("USERS");
        return this;
    }

    public void WithCount()
    {
        _patterns.Add("RETURN count(*) AS count");
    }

    public IAllFieldsUser AsCurrentUser()
    {
        _patterns.Add("CURRENT USER");
        return this;
    }

    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (showUserlauses.Length > 0)
        {
            showUserlauses.Append(' ');
        }

        showUserlauses.Append("SHOW ");
        showUserlauses.Append(string.Join(" ", _patterns));
    }
}