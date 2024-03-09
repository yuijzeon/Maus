using System.Collections.Specialized;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

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
        return obj.GetType().GetProperties().ToDictionary(p =>
        {
            var bindProperty = p.GetCustomAttribute<BindPropertyAttribute>();
            var jsonProperty = p.GetCustomAttribute<JsonPropertyNameAttribute>();
            return bindProperty?.Name ?? jsonProperty?.Name ?? p.Name;
        }, p => p.GetValue(obj)?.ToString());
    }

    public static T ParseTo<T>(this NameValueCollection obj)
    {
        var result = Activator.CreateInstance<T>();
        var propertyInfos = typeof(T).GetProperties();
        foreach (var propertyInfo in propertyInfos)
        {
            var bindProperty = propertyInfo.GetCustomAttribute<BindPropertyAttribute>();
            var jsonProperty = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var key = bindProperty?.Name ?? jsonProperty?.Name ?? propertyInfo.Name;
            var value = obj[key];
            if (value != null)
            {
                propertyInfo.SetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType));
            }
        }

        return result;
    }

    public static string ToSha256String(this string value)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}