using System.Web;
using Maus.Server.Extensions;

namespace Maus.Server.Payment.EcPay.Utils;

public static class EcPayHelper
{
    public static string GenerateSignature(object obj, string hashKey, string hashIv)
    {
        var keyValuePairs = ((List<KeyValuePair<string, string>>)
        [
            new KeyValuePair<string, string>("HashKey", hashKey),
            ..obj.ToStringDictionary()
                .Where(x => x.Key != "CheckMacValue")
                .OrderBy(x => x.Key),
            new KeyValuePair<string, string>("HashIV", hashIv)
        ]).Select(x => $"{x.Key}={x.Value}");

        var preSignature = string.Join("&", keyValuePairs);

        return HttpUtility.UrlEncode(preSignature).ToLower().ToSha256String().ToUpper();
    }
}