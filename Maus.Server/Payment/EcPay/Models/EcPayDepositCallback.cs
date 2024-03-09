using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment.EcPay.Models;

public class EcPayDepositCallback
{
    [BindProperty(Name = "MerchantID")]
    public string MerchantId { get; set; }

    [BindProperty(Name = "MerchantTradeNo")]
    public string MerchantTradeNo { get; set; }

    [BindProperty(Name = "StoreID")]
    public string StoreId { get; set; }

    [BindProperty(Name = "RtnCode")]
    public int RtnCode { get; set; }

    [BindProperty(Name = "RtnMsg")]
    public string RtnMsg { get; set; }

    [BindProperty(Name = "TradeNo")]
    public string TradeNo { get; set; }

    [BindProperty(Name = "TradeAmt")]
    public int TradeAmt { get; set; }

    [BindProperty(Name = "PaymentDate")]
    public string PaymentDate { get; set; }

    [BindProperty(Name = "PaymentType")]
    public string PaymentType { get; set; }

    [BindProperty(Name = "PaymentTypeChargeFee")]
    public int PaymentTypeChargeFee { get; set; }

    [BindProperty(Name = "TradeDate")]
    public string TradeDate { get; set; }

    [BindProperty(Name = "SimulatePaid")]
    public int SimulatePaid { get; set; }

    [BindProperty(Name = "CustomField1")]
    public string CustomField1 { get; set; }

    [BindProperty(Name = "CustomField2")]
    public string CustomField2 { get; set; }

    [BindProperty(Name = "CustomField3")]
    public string CustomField3 { get; set; }

    [BindProperty(Name = "CustomField4")]
    public string CustomField4 { get; set; }

    [BindProperty(Name = "CheckMacValue")]
    public string CheckMacValue { get; set; }
}