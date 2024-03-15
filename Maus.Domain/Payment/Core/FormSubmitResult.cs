namespace Maus.Domain.Payment.Core;

public class FormSubmitResult : IPaymentResult
{
    public required string ActionUrl { get; set; }
    public required Dictionary<string, string> FormData { get; set; }
    public required PaymentResultType Type { get; set; }
}