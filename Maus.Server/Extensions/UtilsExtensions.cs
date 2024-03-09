using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Maus.Server.Extensions;

public static class UtilsExtensions
{
    public static string ToJsonString(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public static T FromJson<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json) ?? default(T);
    }

    public static Dictionary<string, string> ToStringDictionary(this object obj)
    {
        var dictionary = obj.ToJsonString()
            .FromJson<Dictionary<string, object>>()?
            .ToDictionary(x => x.Key, x => x.Value?.ToString() ?? string.Empty);
        return dictionary ?? new Dictionary<string, string>();
    }

    public static string ToSha256String(this string value)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}