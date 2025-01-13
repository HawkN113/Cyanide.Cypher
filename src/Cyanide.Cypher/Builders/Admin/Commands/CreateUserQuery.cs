using System.Text;

namespace Cyanide.Cypher.Builders.Admin.Commands;

public sealed class CreateUserQuery(StringBuilder createUserClauses) : ICreateAdmQueryUser, ISetUserPassword, ISetUserStatus, ISetUserHomeDb
{
    private readonly List<string> _patterns = [];
    private bool _shouldReplaced;
    
    public ISetUserPassword WithUser(string userName)
    {
        _patterns.Add($"USER {userName}");
        return this;
    }

    public void Replace()
    {
        _shouldReplaced = true;
    }

    public ISetUserHomeDb SetStatus(UserStatus status = UserStatus.ACTIVE)
    {
        _patterns.Add($"SET STATUS {status.ToString()}");
        return this;
    }

    public ISetUserStatus WithPassword(string password, PasswordType type = PasswordType.PLAINTEXT,
        bool changeRequired = false)
    {
        var change = !changeRequired ? "CHANGE NOT REQUIRED" : "CHANGE REQUIRED";
        _patterns.Add($"SET {type.ToString()} PASSWORD {password} {change}");
        return this;
    }

    public IReplaceUser SetHomeDb(string databaseName)
    {
        _patterns.Add($"SET HOME DATABASE {databaseName}");
        return this;
    }
    
    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (createUserClauses.Length > 0)
        {
            createUserClauses.Append(' ');
        }
        createUserClauses.Append(!_shouldReplaced ? "CREATE " : "CREATE OR REPLACE ");
        createUserClauses.Append(string.Join(" ", _patterns));
    }
}