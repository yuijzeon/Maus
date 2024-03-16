namespace Maus.Domain.Payment.Core;

public class PaymentChannel
{
    public ProviderCode ProviderCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public string ProviderMethodCode { get; set; }
    public string ProviderMerchantCode { get; set; }
    public string HashKey { get; set; }
    public string HashIv { get; set; }
    public string SubmitUrl { get; set; }
    public string CallbackUrl { get; set; }
    public string QueryUrl { get; set; }
}