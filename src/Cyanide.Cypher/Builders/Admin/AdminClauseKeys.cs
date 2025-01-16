using System.ComponentModel;
using System.Runtime.Serialization;

namespace Cyanide.Cypher.Builders.Admin;

[DataContract]
internal enum AdminClauseKeys
{
    [Description("CreateDb")] CreateDb = 0,
    [Description("CreateUser")] CreateUser = 1,
    [Description("ShowDb")] ShowDb = 2,
    [Description("ShowUser")] ShowUser = 3
}