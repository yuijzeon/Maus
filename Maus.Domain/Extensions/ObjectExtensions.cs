using System.Text.Json;

namespace Maus.Domain.Extensions;

public static class ObjectExtensions
{
    public static Dictionary<string, string> ToStringDictionary(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
    }
}