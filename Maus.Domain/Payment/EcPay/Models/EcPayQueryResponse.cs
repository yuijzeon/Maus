using System.Web;
using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Enums;
using Maus.Domain.Payment.EcPay.Utils;

namespace Maus.Domain.Payment.EcPay.Models;

public class EcPayQueryResponse
{
    public EcPayQueryResponse(string queryString)
    {
        var collection = HttpUtility.ParseQueryString(queryString);
        MerchantId = collection["MerchantID"];
        MerchantTradeNo = collection["MerchantTradeNo"];
        StoreId = collection["StoreID"];
        TradeNo = collection["TradeNo"];
        TradeAmt = collection["TradeAmt"].ParseOrDefault<int>();
        PaymentDate = collection["PaymentDate"].ParseOrDefault<DateTime>();
        PaymentType = collection["PaymentType"];
        HandlingCharge = collection["HandlingCharge"].ParseOrDefault<int>();
        PaymentTypeChargeFee = collection["PaymentTypeChargeFee"].ParseOrDefault<int>();
        TradeDate = collection["TradeDate"].ParseOrDefault<DateTime>();
        TradeStatus = collection["TradeStatus"].ToEnumOrDefault<EcPayStatus>();
        ItemName = collection["ItemName"];
        CustomField1 = collection["CustomField1"];
        CustomField2 = collection["CustomField2"];
        CustomField3 = collection["CustomField3"];
        CustomField4 = collection["CustomField4"];
        CheckMacValue = collection["CheckMacValue"];
    }

    public string? MerchantId { get; private set; }
    public string? MerchantTradeNo { get; private set; }
    public string? StoreId { get; private set; }
    public string? TradeNo { get; private set; }
    public int? TradeAmt { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public string? PaymentType { get; private set; }
    public int HandlingCharge { get; private set; }
    public int PaymentTypeChargeFee { get; private set; }
    public DateTime TradeDate { get; private set; }
    public EcPayStatus TradeStatus { get; private set; }
    public string? ItemName { get; private set; }
    public string? CustomField1 { get; private set; }
    public string? CustomField2 { get; private set; }
    public string? CustomField3 { get; private set; }
    public string? CustomField4 { get; private set; }
    public string? CheckMacValue { get; }

    public void CheckSignature(string hashKey, string hashIv)
    {
        var signature = EcPayHelper.GenerateSignature(this, hashKey, hashIv);

        if (signature != CheckMacValue)
        {
            throw new PaymentException("Invalid signature");
        }
    }
}