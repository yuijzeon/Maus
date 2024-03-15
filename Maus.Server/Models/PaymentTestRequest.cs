using Maus.Domain.Payment.Core;

namespace Maus.Server.Models;

public class PaymentTestRequest
{
    public required string TransactionNo { get; set; }
    public PaymentProvider PaymentProvider { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public decimal RequestAmount { get; set; }
}