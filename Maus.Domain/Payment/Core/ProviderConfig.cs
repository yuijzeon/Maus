namespace Maus.Domain.Payment.Core;

public class ProviderConfig
{
    public ProviderCode ProviderCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public BankCode BankCode { get; set; }
    public string? ProviderMethodCode { get; set; }
    public string? ProviderBankCode { get; set; }
    public UrlConfig UrlConfig { get; set; }
}