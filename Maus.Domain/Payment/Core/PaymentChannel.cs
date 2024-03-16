﻿namespace Maus.Domain.Payment.Core;

public class PaymentChannel
{
    public ProviderCode ProviderCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public string MerchantCode { get; set; }
    public string MerchantKey { get; set; }
    public string MerchantIv { get; set; }
    public string SubmitUrl { get; set; }
    public string CallbackUrl { get; set; }
    public string QueryUrl { get; set; }
}