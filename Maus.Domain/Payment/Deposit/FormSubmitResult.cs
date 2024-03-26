namespace Maus.Domain.Payment.Deposit;

public class FormSubmitResult : IPaymentResult
{
    public required string ActionUrl { get; set; }
    public required Dictionary<string, string> FormData { get; set; }
    public PaymentResultType Type => PaymentResultType.FormSubmit;
}