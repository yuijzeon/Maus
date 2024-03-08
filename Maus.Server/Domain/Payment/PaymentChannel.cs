namespace Maus.Server.Domain.Payment;

public class PaymentChannel
{
    public PaymentProvider PaymentProvider { get; set; }
    public required string MerchantCode { get; set; }
    public required string MerchantKey { get; set; }
    public required string SubmitUrl { get; set; }
    public required string CallbackUrl { get; set; }
    public string MerchantHashIv { get; set; } = string.Empty;
}