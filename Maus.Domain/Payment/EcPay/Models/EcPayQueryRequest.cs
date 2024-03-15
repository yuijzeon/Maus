using System.Text.Json.Serialization;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Utils;

namespace Maus.Domain.Payment.EcPay.Models;

public class EcPayQueryRequest
{
    public EcPayQueryRequest(string transactionNo, PaymentChannel paymentChannel)
    {
        MerchantId = paymentChannel.MerchantCode;
        MerchantTradeNo = transactionNo;
        TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        CheckMacValue = EcPayHelper.GenerateSignature(this, paymentChannel.MerchantKey, paymentChannel.MerchantIv);
    }

    [JsonPropertyName("MerchantID")]
    public string? MerchantId { get; set; }

    [JsonPropertyName("MerchantTradeNo")]
    public string MerchantTradeNo { get; set; }

    [JsonPropertyName("TimeStamp")]
    public long TimeStamp { get; set; }

    [JsonPropertyName("CheckMacValue")]
    public string CheckMacValue { get; set; }

    [JsonPropertyName("PlatformID")]
    public string? PlatformId { get; set; }
}