namespace Maus.Domain.Payment.Core;

public class PaymentTransaction
{
    public string TransactionNo { get; set; }
    public PaymentProvider PaymentProvider { get; set; }
    public decimal RequestAmount { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}