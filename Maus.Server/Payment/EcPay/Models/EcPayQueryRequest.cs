using Maus.Server.Payment.EcPay.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment.EcPay.Models;

public class EcPayQueryRequest
{
    public EcPayQueryRequest(string transactionNo, PaymentChannel paymentChannel)
    {
        MerchantId = paymentChannel.MerchantCode;
        MerchantTradeNo = transactionNo;
        TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        CheckMacValue = EcPayHelper.GenerateSignature(this, paymentChannel.MerchantKey, paymentChannel.MerchantIv);
    }

    [BindProperty(Name = "MerchantID")]
    public string MerchantId { get; set; }

    [BindProperty(Name = "MerchantTradeNo")]
    public string MerchantTradeNo { get; set; }

    [BindProperty(Name = "TimeStamp")]
    public long TimeStamp { get; set; }

    [BindProperty(Name = "CheckMacValue")]
    public string CheckMacValue { get; set; }

    [BindProperty(Name = "PlatformID")]
    public string PlatformId { get; set; }
}