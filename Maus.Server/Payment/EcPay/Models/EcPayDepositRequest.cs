using System.Text.Json.Serialization;
using ECPay.Payment.Integration;
using Maus.Server.Payment.EcPay.Utils;

namespace Maus.Server.Payment.EcPay.Models;

public class EcPayDepositRequest(PaymentChannel paymentChannel, Transaction transaction)
{
    [JsonPropertyName("MerchantID")]
    public string MerchantId { get; set; } = paymentChannel.MerchantCode;

    [JsonPropertyName("MerchantTradeNo")]
    public string MerchantTradeNo { get; set; } = transaction.TransactionNo;

    [JsonPropertyName("MerchantTradeDate")]
    public string MerchantTradeDate { get; set; } = transaction.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");

    [JsonPropertyName("PaymentType")]
    public string PaymentType { get; set; } = "aio";

    [JsonPropertyName("TotalAmount")]
    public int TotalAmount { get; set; } = (int)transaction.RequestAmount;

    [JsonPropertyName("TradeDesc")]
    public string TradeDesc { get; set; } = "交易描述";

    [JsonPropertyName("ItemName")]
    public string ItemName { get; set; } = "testing";

    [JsonPropertyName("ReturnURL")]
    public string ReturnUrl { get; set; } = paymentChannel.CallbackUrl;

    [JsonPropertyName("ChoosePayment")]
    public string ChoosePayment { get; set; } = PaymentMethod.ALL.ToString();

    [JsonPropertyName("CheckMacValue")]
    public string CheckMacValue { get; set; }

    [JsonPropertyName("EncryptType")]
    public int EncryptType { get; set; } = 1;

    public void GenerateSignature(string hashKey, string hashIv)
    {
        CheckMacValue = EcPayHelper.GenerateSignature(this, hashKey, hashIv);
    }
}