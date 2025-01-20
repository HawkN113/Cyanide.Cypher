using System.ComponentModel;
using System.Runtime.Serialization;

namespace Cyanide.Cypher.Builders.AdminQuery;

[DataContract]
internal enum AdminClauseKeys
{
    [Description("CreateDb")] CreateDb = 0,
    [Description("CreateUser")] CreateUser = 1,
    [Description("ShowDb")] ShowDb = 2,
    [Description("ShowUser")] ShowUser = 3,
    [Description("UpdateDb")] UpdateDb = 4,
    [Description("StartDb")] StartDb = 5,
    [Description("StopDb")] StopDb = 6,
    [Description("DeleteDb")] DeleteDb = 7
}