using System.ComponentModel;

namespace Cyanide.Cypher.Builders;

public enum UserStatus
{
    [Description("ACTIVE")] Active = 0,
    [Description("SUSPENDED")] Suspended = 1
}