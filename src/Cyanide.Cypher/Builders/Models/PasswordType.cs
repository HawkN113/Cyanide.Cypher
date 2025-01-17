using System.ComponentModel;

namespace Cyanide.Cypher.Builders.Models;

public enum PasswordType
{
    [Description("PLAINTEXT")] Plaintext = 0,
    [Description("ENCRYPTED")] Encrypted = 1
}