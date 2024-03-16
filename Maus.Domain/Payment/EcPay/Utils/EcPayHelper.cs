using System.Web;
using Maus.Domain.Extensions;

namespace Maus.Domain.Payment.EcPay.Utils;

public static class EcPayHelper
{
    public static string GenerateSignature(IEnumerable<KeyValuePair<string, string>> keyValuePairs, string hashKey, string hashIv)
    {
        var preSignature = string.Join("&", ((List<KeyValuePair<string, string>>)
        [
            new KeyValuePair<string, string>("HashKey", hashKey),
            ..keyValuePairs
                .Where(x => x.Key != "CheckMacValue")
                .OrderBy(x => x.Key),
            new KeyValuePair<string, string>("HashIV", hashIv)
        ]).Select(x => $"{x.Key}={x.Value}"));

        return HttpUtility.UrlEncode(preSignature).ToLower().ToSha256String().ToUpper();
    }
}