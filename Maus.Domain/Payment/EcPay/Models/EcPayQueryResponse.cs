using System.Text.Json.Serialization;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Utils;

namespace Maus.Domain.Payment.EcPay.Models;

public class EcPayQueryResponse
{
    [JsonPropertyName("MerchantID")]
    public string? MerchantId { get; set; }

    [JsonPropertyName("MerchantTradeNo")]
    public string? MerchantTradeNo { get; set; }

    [JsonPropertyName("StoreID")]
    public string? StoreId { get; set; }

    [JsonPropertyName("TradeNo")]
    public string? TradeNo { get; set; }

    [JsonPropertyName("TradeAmt")]
    public int TradeAmt { get; set; }

    [JsonPropertyName("PaymentDate")]
    public string? PaymentDate { get; set; }

    [JsonPropertyName("PaymentType")]
    public string? PaymentType { get; set; }

    [JsonPropertyName("HandlingCharge")]
    public int HandlingCharge { get; set; }

    [JsonPropertyName("PaymentTypeChargeFee")]
    public int PaymentTypeChargeFee { get; set; }

    [JsonPropertyName("TradeDate")]
    public string? TradeDate { get; set; }

    [JsonPropertyName("TradeStatus")]
    public string? TradeStatus { get; set; }

    [JsonPropertyName("ItemName")]
    public string? ItemName { get; set; }

    [JsonPropertyName("CustomField1")]
    public string? CustomField1 { get; set; }

    [JsonPropertyName("CustomField2")]
    public string? CustomField2 { get; set; }

    [JsonPropertyName("CustomField3")]
    public string? CustomField3 { get; set; }

    [JsonPropertyName("CustomField4")]
    public string? CustomField4 { get; set; }

    [JsonPropertyName("CheckMacValue")]
    public string? CheckMacValue { get; set; }

    public void CheckSignature(string hashKey, string hashIv)
    {
        var signature = EcPayHelper.GenerateSignature(this, hashKey, hashIv);

        if (signature != CheckMacValue)
        {
            throw new PaymentException("Invalid signature");
        }
    }
}