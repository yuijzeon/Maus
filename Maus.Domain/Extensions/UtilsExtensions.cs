using System.Collections.Specialized;

namespace Maus.Domain.Extensions;

public static class UtilsExtensions
{
    public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this NameValueCollection collection)
    {
        return collection.AllKeys.SelectMany(x =>
        {
            if (x is null)
            {
                return [];
            }

            var values = collection.GetValues(x);
            return values is null
                ? [new KeyValuePair<string, string>(x, string.Empty)]
                : values.Select(y => new KeyValuePair<string, string>(x, y));
        });
    }
}