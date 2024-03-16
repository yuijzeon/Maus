using System.Text.Json.Serialization;
using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Utils;

namespace Maus.Domain.Payment.EcPay.Models;

public class EcPayDepositRequest
{
    public EcPayDepositRequest(PaymentTransaction transaction, PaymentChannel channel)
    {
        MerchantId = channel.ProviderMerchantCode;
        MerchantTradeNo = transaction.MerchantTransactionNo;
        MerchantTradeDate = transaction.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");
        PaymentType = "aio";
        TotalAmount = (int)transaction.RequestAmount;
        TradeDesc = transaction.Remark ?? "-";
        ItemName = transaction.ItemName ?? "-";
        ReturnUrl = channel.CallbackUrl;
        ChoosePayment = channel.ProviderMethodCode;
        EncryptType = 1;
        SetSubPayment(transaction, channel);
        CheckMacValue = EcPayHelper.GenerateSignature(this.ToStringDictionary(), channel.HashKey, channel.HashIv);
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

    [JsonPropertyName("ChooseSubPayment")]
    public string? ChooseSubPayment { get; set; }

    [JsonPropertyName("UnionPay")]
    public int? UnionPay { get; set; }

    private void SetSubPayment(PaymentTransaction transaction, PaymentChannel channel)
    {
        if (transaction.MethodCode is MethodCode.CreditCard)
        {
            UnionPay = channel.ProviderBankCode.ParseOrNull<int>();
        }
        else
        {
            ChooseSubPayment = channel.ProviderBankCode;
        }
    }
}