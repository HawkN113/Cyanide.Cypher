using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin.Commands;

public sealed class ShowUserQuery :
    IBuilderInitializer,
    IAllFieldsUser,
    IShowAllUsers,
    IFieldsCountUser,
    ICurrentUser
{
    private readonly List<string> _patterns = [];
    private StringBuilder _showUserClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _showUserClauses = clauseBuilder;
    }

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

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_showUserClauses.Length > 0)
        {
            _showUserClauses.Append(' ');
        }

        _showUserClauses.Append("SHOW ");
        _showUserClauses.Append(string.Join(" ", _patterns));
    }
}