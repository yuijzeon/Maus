using System.Text.Json.Serialization;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Utils;

namespace Maus.Domain.Payment.EcPay.Models;

public class EcPayDepositRequest
{
    public EcPayDepositRequest(PaymentChannel channel, PaymentTransaction transaction)
    {
        MerchantId = channel.MerchantCode;
        MerchantTradeNo = transaction.TransactionNo;
        MerchantTradeDate = transaction.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");
        PaymentType = "aio";
        TotalAmount = (int)transaction.RequestAmount;
        TradeDesc = "交易描述";
        ItemName = transaction.ItemName ?? "-";
        ReturnUrl = channel.CallbackUrl;
        ChoosePayment = "ALL";
        EncryptType = 1;
        CheckMacValue = EcPayHelper.GenerateSignature(this, channel.MerchantKey, channel.MerchantIv);
    }

    [JsonPropertyName("MerchantID")]
    public string MerchantId { get; private set; }

    [JsonPropertyName("MerchantTradeNo")]
    public string MerchantTradeNo { get; private set; }

    [JsonPropertyName("MerchantTradeDate")]
    public string MerchantTradeDate { get; private set; }

    [JsonPropertyName("PaymentType")]
    public string PaymentType { get; private set; }

    [JsonPropertyName("TotalAmount")]
    public int TotalAmount { get; private set; }

    [JsonPropertyName("TradeDesc")]
    public string TradeDesc { get; private set; }

    [JsonPropertyName("ItemName")]
    public string ItemName { get; private set; }

    [JsonPropertyName("ReturnURL")]
    public string ReturnUrl { get; private set; }

    [JsonPropertyName("ChoosePayment")]
    public string ChoosePayment { get; private set; }

    [JsonPropertyName("CheckMacValue")]
    public string CheckMacValue { get; private set; }

    [JsonPropertyName("EncryptType")]
    public int EncryptType { get; private set; }
}