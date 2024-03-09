using System.Text.Json.Serialization;
using System.Web;
using ECPay.Payment.Integration;
using Maus.Server.Extensions;

namespace Maus.Server.Payment.EcPay.Models;

public class EcPayAioDepositRequest(PaymentChannel paymentChannel, OrderDetail orderDetail)
{
    [JsonPropertyName("MerchantID")]
    public string MerchantId { get; set; } = paymentChannel.MerchantCode;

    [JsonPropertyName("MerchantTradeNo")]
    public string MerchantTradeNo { get; set; } = orderDetail.OrderNo;

    [JsonPropertyName("MerchantTradeDate")]
    public string MerchantTradeDate { get; set; } = orderDetail.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");

    [JsonPropertyName("PaymentType")]
    public string PaymentType { get; set; } = "aio";

    [JsonPropertyName("TotalAmount")]
    public int TotalAmount { get; set; } = (int)orderDetail.RequestAmount;

    [JsonPropertyName("TradeDesc")]
    public string TradeDesc { get; set; } = "交易描述";

    [JsonPropertyName("ItemName")]
    public string ItemName { get; set; } = "testing";

    [JsonPropertyName("ReturnURL")]
    public string ReturnUrl { get; set; } = paymentChannel.CallbackUrl;

    [JsonPropertyName("ChoosePayment")]
    public string ChoosePayment { get; set; } = PaymentMethod.ALL.ToString();

    [JsonPropertyName("CheckMacValue")]
    public string? CheckMacValue { get; set; }

    [JsonPropertyName("EncryptType")]
    public int EncryptType { get; set; } = 1;

    public void GenerateSignature(string hashKey, string hashIv)
    {
        var keyValuePairs = ((List<KeyValuePair<string, string>>)
        [
            new KeyValuePair<string, string>("HashKey", hashKey),
            ..this.ToStringDictionary()
                .Where(x => x.Key != "CheckMacValue")
                .OrderBy(x => x.Key),
            new KeyValuePair<string, string>("HashIV", hashIv)
        ]).Select(x => $"{x.Key}={x.Value}");

        var preSignature = string.Join("&", keyValuePairs);

        CheckMacValue = HttpUtility.UrlEncode(preSignature).ToLower().ToSha256String().ToUpper();
    }
}