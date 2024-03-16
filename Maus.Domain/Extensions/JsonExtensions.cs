using System.Text.Json;

namespace Maus.Domain.Extensions;

public static class JsonExtensions
{
    public static string ToJsonString(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public static T ParseJson<T>(this string json)
    {
        var deserialize = JsonSerializer.Deserialize<T>(json);

        if (deserialize == null)
        {
            throw new JsonException();
        }

        return deserialize;
    }

    public static Dictionary<string, string> ToStringDictionary(this object obj)
    {
        return obj.ToJsonString().ParseJson<Dictionary<string, object?>>()
            .ToDictionary(k => k.Key, v => v.Value?.ToString() ?? string.Empty);
    }
}