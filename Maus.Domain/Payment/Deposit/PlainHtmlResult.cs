namespace Maus.Domain.Payment.Deposit;

public class PlainHtmlResult : IPaymentResult
{
    public required string HtmlString { get; set; }
    public PaymentResultType Type => PaymentResultType.PlainHtml;
}