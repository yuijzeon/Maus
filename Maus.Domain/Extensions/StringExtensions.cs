namespace Maus.Domain.Extensions;

public static class StringExtensions
{
    public static T Parse<T>(this string value) where T : struct, ISpanParsable<T>
    {
        return T.Parse(value, null);
    }

    public static T ParseOrDefault<T>(this string? value) where T : struct, ISpanParsable<T>
    {
        return T.TryParse(value, null, out var result) ? result : default;
    }

    public static T? ParseOrNull<T>(this string? value) where T : struct, ISpanParsable<T>
    {
        return T.TryParse(value, null, out var result) ? result : null;
    }

    public static T ToEnum<T>(this string value) where T : struct, Enum
    {
        return Enum.Parse<T>(value);
    }

    public static T ToEnumOrDefault<T>(this string? value) where T : struct, Enum
    {
        return Enum.TryParse(value, out T result) ? result : default;
    }

    public static T? ToEnumOrNull<T>(this string? value) where T : struct, Enum
    {
        return Enum.TryParse(value, out T result) ? result : null;
    }
}