using System.Text.Json.Serialization;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Utils;

namespace Maus.Domain.Payment.EcPay.Models;

public class EcPayDepositRequest
{
    public EcPayDepositRequest(PaymentChannel paymentChannel, PaymentTransaction paymentTransaction)
    {
        MerchantId = paymentChannel.MerchantCode;
        MerchantTradeNo = paymentTransaction.TransactionNo;
        MerchantTradeDate = paymentTransaction.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");
        PaymentType = "aio";
        TotalAmount = (int)paymentTransaction.RequestAmount;
        TradeDesc = "交易描述";
        ItemName = "testing";
        ReturnUrl = paymentChannel.CallbackUrl;
        ChoosePayment = "ALL";
        EncryptType = 1;
        CheckMacValue = EcPayHelper.GenerateSignature(this, paymentChannel.MerchantKey, paymentChannel.MerchantIv);
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