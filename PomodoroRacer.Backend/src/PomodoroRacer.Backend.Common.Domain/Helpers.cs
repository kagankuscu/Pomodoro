namespace PomodoroRacer.Backend.Common.Domain;

public static class StringHelpers
{
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return char.ToLowerInvariant(str[0]) + str[1..];
    }
}