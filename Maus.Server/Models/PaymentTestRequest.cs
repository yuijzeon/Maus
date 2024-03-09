using Maus.Server.Payment;

namespace Maus.Server.Models;

public class PaymentTestRequest
{
    public string TransactionNo { get; set; }
    public PaymentProvider PaymentProvider { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public decimal RequestAmount { get; set; }
}