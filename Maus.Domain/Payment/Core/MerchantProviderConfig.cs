namespace Maus.Domain.Payment.Core;

public class MerchantProviderConfig
{
    public ProviderCode ProviderCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public string ProviderMerchantCode { get; set; }
    public string ProviderMerchantKey { get; set; }
    public string ProviderMerchantIv { get; set; }
}