using ECPay.Payment.Integration;
using Maus.Server.Payment.EcPay.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment.EcPay.Models;

public class EcPayDepositRequest
{
    public EcPayDepositRequest(PaymentChannel paymentChannel, Transaction transaction)
    {
        MerchantId = paymentChannel.MerchantCode;
        MerchantTradeNo = transaction.TransactionNo;
        MerchantTradeDate = transaction.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");
        PaymentType = "aio";
        TotalAmount = (int)transaction.RequestAmount;
        TradeDesc = "交易描述";
        ItemName = "testing";
        ReturnUrl = paymentChannel.CallbackUrl;
        ChoosePayment = PaymentMethod.ALL.ToString();
        EncryptType = 1;
        CheckMacValue = EcPayHelper.GenerateSignature(this, paymentChannel.MerchantKey, paymentChannel.MerchantIv);
    }

    [BindProperty(Name = "MerchantID")]
    public string MerchantId { get; set; }

    [BindProperty(Name = "MerchantTradeNo")]
    public string MerchantTradeNo { get; set; }

    [BindProperty(Name = "MerchantTradeDate")]
    public string MerchantTradeDate { get; set; }

    [BindProperty(Name = "PaymentType")]
    public string PaymentType { get; set; }

    [BindProperty(Name = "TotalAmount")]
    public int TotalAmount { get; set; }

    [BindProperty(Name = "TradeDesc")]
    public string TradeDesc { get; set; }

    [BindProperty(Name = "ItemName")]
    public string ItemName { get; set; }

    [BindProperty(Name = "ReturnURL")]
    public string ReturnUrl { get; set; }

    [BindProperty(Name = "ChoosePayment")]
    public string ChoosePayment { get; set; }

    [BindProperty(Name = "CheckMacValue")]
    public string CheckMacValue { get; set; }

    [BindProperty(Name = "EncryptType")]
    public int EncryptType { get; set; }
}