using System.Text.Json.Serialization;
using System.Web;
using ECPay.Payment.Integration;
using Maus.Server.Extensions;

namespace Maus.Server.Payment.EcPay.Models;

public class EcPayAioDepositRequest
{
    public EcPayAioDepositRequest(PaymentChannel paymentChannel, OrderDetail orderDetail)
    {
        MerchantId = paymentChannel.MerchantCode;
        MerchantTradeNo = orderDetail.OrderNo;
        MerchantTradeDate = orderDetail.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");
        PaymentType = "aio";
        TotalAmount = (int)orderDetail.RequestAmount;
        TradeDesc = "交易描述";
        ItemName = "testing";
        ReturnUrl = paymentChannel.CallbackUrl;
        ChoosePayment = PaymentMethod.ALL.ToString();
        EncryptType = 1;
        CheckMacValue = GenerateSignature(paymentChannel);
    }

    [JsonPropertyName("MerchantID")]
    public string MerchantId { get; set; }

    [JsonPropertyName("MerchantTradeNo")]
    public string MerchantTradeNo { get; set; }

    [JsonPropertyName("MerchantTradeDate")]
    public string MerchantTradeDate { get; set; }

    [JsonPropertyName("PaymentType")]
    public string PaymentType { get; set; }

    [JsonPropertyName("TotalAmount")]
    public int TotalAmount { get; set; }

    [JsonPropertyName("TradeDesc")]
    public string TradeDesc { get; set; }

    [JsonPropertyName("ItemName")]
    public string ItemName { get; set; }

    [JsonPropertyName("ReturnURL")]
    public string ReturnUrl { get; set; }

    [JsonPropertyName("ChoosePayment")]
    public string ChoosePayment { get; set; }

    [JsonPropertyName("CheckMacValue")]
    public string CheckMacValue { get; set; }

    [JsonPropertyName("EncryptType")]
    public int EncryptType { get; set; }

    private string GenerateSignature(PaymentChannel paymentChannel)
    {
        var keyValuePairs = ((List<KeyValuePair<string, string>>)
        [
            new KeyValuePair<string, string>("HashKey", paymentChannel.MerchantKey),
            ..this.ToStringDictionary()
                .Where(x => x.Key != "CheckMacValue")
                .OrderBy(x => x.Key),
            new KeyValuePair<string, string>("HashIV", paymentChannel.MerchantIv)
        ]).Select(x => $"{x.Key}={x.Value}");

        var preSignature = string.Join("&", keyValuePairs);

        return HttpUtility.UrlEncode(preSignature).ToLower().ToSha256String().ToUpper();
    }
}