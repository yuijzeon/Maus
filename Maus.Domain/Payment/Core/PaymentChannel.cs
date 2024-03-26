namespace Maus.Domain.Payment.Core;

public class PaymentChannel
{
    public string ProviderMerchantCode { get; set; }
    public string HashKey { get; set; }
    public string HashIv { get; set; }
    public ProviderCode ProviderCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public BankCode? BankCode { get; set; }
    public string? ProviderMethodCode { get; set; }
    public string? ProviderBankCode { get; set; }
    public string SubmitUrl { get; set; }
    public string CallbackUrl { get; set; }
    public string QueryUrl { get; set; }
}