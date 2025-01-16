using System.ComponentModel;
using System.Runtime.Serialization;

namespace Cyanide.Cypher.Builders.Query;

[DataContract]
internal enum QueryClauseKeys
{
    [Description("Match")] Match = 0,
    [Description("OptionalMatch")] OptionalMatch = 1,
    [Description("Where")] Where = 2,
    [Description("Create")] Create = 3,
    [Description("Delete")] Delete = 4,
    [Description("DetachDelete")] DetachDelete = 5,
    [Description("Remove")] Remove = 6,
    [Description("Set")] Set = 7,
    [Description("With")] With = 8,
    [Description("Return")] Return = 9,
    [Description("OrderBy")] OrderBy = 10,
    [Description("Skip")] Skip = 11,
    [Description("Limit")] Limit = 12
}