using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Maus.Domain.Extensions;

public static class UtilsExtensions
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

    public static T ParseJson<T>(this NameValueCollection obj)
    {
        return obj.Cast<string>().ToDictionary(x => x, x => obj[x]).ToJsonString().ParseJson<T>();
    }

    public static Dictionary<string, string> ToStringDictionary(this object obj)
    {
        return obj.ToJsonString().ParseJson<Dictionary<string, object?>>()
            .Where(x => x.Value != null)
            .ToDictionary(k => k.Key, v => v.Value?.ToString() ?? string.Empty);
    }

    public static string ToSha256String(this string value)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}