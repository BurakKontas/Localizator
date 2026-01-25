namespace Localizator.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhitespace(this string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static string Format(this string str, params string[] args)
    {
        return string.Format(str, args);
    }

    public static int AsInt(this string? str)
    {
        return str.IsNullOrWhitespace() ? 0 : int.Parse(str!);
    }

    public static long AsLong(this string str)
    {
        return str.IsNullOrWhitespace() ? 0L : long.Parse(str!);
    }

    public static double AsDouble(this string str)
    {
        return str.IsNullOrWhitespace() ? 0.0d : double.Parse(str!);
    }

    public static float AsFloat(this string str)
    {
        return str.IsNullOrWhitespace() ? 0.0f : float.Parse(str!);
    }

    public static decimal AsDecimal(this string str)
    {
        return str.IsNullOrWhitespace() ? 0.0m : decimal.Parse(str!);
    }
}
