using System.ComponentModel.DataAnnotations;

namespace AllResultsPattern.MyCustomResults;

public enum MessageType
{
    Success,

    Information,

    Warning,

    //Failure
}

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        DisplayAttribute? displayAttribute = enumValue.GetType().GetField(enumValue.ToString())?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? enumValue.ToString();
    }
}