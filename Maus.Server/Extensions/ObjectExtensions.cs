using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Maus.Server.Extensions;

public static class ObjectExtensions
{
    public static Dictionary<string, string> ToStringDictionary(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, object?>>(json)?
            .ToDictionary(x => x.Key, x => x.Value?.ToString() ?? string.Empty);
        return dictionary ?? new Dictionary<string, string>();
    }

    public static string ToSha256String(this string value)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}