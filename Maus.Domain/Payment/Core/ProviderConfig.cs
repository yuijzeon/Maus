namespace Maus.Domain.Payment.Core;

public class ProviderConfig
{
    public ProviderCode ProviderCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public string SubmitUrl { get; set; }
    public string CallbackUrl { get; set; }
    public string QueryUrl { get; set; }
}