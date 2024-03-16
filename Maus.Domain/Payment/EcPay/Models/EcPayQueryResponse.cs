using System.Collections.Specialized;
using System.Web;
using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Enums;
using Maus.Domain.Payment.EcPay.Utils;

namespace Maus.Domain.Payment.EcPay.Models;

public class EcPayQueryResponse
{
    public EcPayQueryResponse(string queryString, PaymentChannel channel)
    {
        var collection = HttpUtility.ParseQueryString(queryString);
        CheckSignature(collection, channel);
        MerchantId = collection.Get("MerchantID");
        MerchantTradeNo = collection.Get("MerchantTradeNo");
        StoreId = collection.Get("StoreID");
        TradeNo = collection.Get("TradeNo");
        TradeAmt = collection.Get("TradeAmt").ParseOrDefault<int>();
        PaymentDate = collection.Get("PaymentDate").ParseOrDefault<DateTime>();
        PaymentType = collection.Get("PaymentType");
        HandlingCharge = collection.Get("HandlingCharge").ParseOrDefault<int>();
        PaymentTypeChargeFee = collection.Get("PaymentTypeChargeFee").ParseOrDefault<int>();
        TradeDate = collection.Get("TradeDate").ParseOrDefault<DateTime>();
        TradeStatus = collection.Get("TradeStatus").ToEnumOrDefault<EcPayStatus>();
        ItemName = collection.Get("ItemName");
        CustomField1 = collection.Get("CustomField1");
        CustomField2 = collection.Get("CustomField2");
        CustomField3 = collection.Get("CustomField3");
        CustomField4 = collection.Get("CustomField4");
        CheckMacValue = collection.Get("CheckMacValue");
    }

    public string? MerchantId { get; }
    public string? MerchantTradeNo { get; }
    public string? StoreId { get; }
    public string? TradeNo { get; }
    public int TradeAmt { get; }
    public DateTime PaymentDate { get; }
    public string? PaymentType { get; }
    public int HandlingCharge { get; }
    public int PaymentTypeChargeFee { get; }
    public DateTime TradeDate { get; }
    public EcPayStatus TradeStatus { get; }
    public string? ItemName { get; }
    public string? CustomField1 { get; }
    public string? CustomField2 { get; }
    public string? CustomField3 { get; }
    public string? CustomField4 { get; }
    public string? CheckMacValue { get; }

    private void CheckSignature(NameValueCollection collection, PaymentChannel channel)
    {
        var keyValuePairs = collection.ToKeyValuePairs();
        var signature = EcPayHelper.GenerateSignature(keyValuePairs, channel.HashKey, channel.HashIv);

        if (signature != collection.Get("CheckMacValue"))
        {
            throw new PaymentException("Invalid signature");
        }
    }

    public bool AlreadyCreated()
    {
        return TradeStatus is not EcPayStatus.UnCreated;
    }
}