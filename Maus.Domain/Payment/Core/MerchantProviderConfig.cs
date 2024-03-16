namespace Maus.Domain.Payment.Core;

public class MerchantProviderConfig
{
    public required string MerchantCode { get; set; }
    public ProviderCode ProviderCode { get; set; }
    public string ProviderMerchantCode { get; set; }
    public string ProviderMerchantKey { get; set; }
    public string ProviderMerchantIv { get; set; }
}