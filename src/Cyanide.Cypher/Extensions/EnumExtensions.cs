using System.ComponentModel;
using System.Reflection;

namespace Cyanide.Cypher.Extensions;

internal static class EnumExtensions
{
    internal static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}