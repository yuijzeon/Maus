using Maus.Domain.Payment.Core;

namespace Maus.Web.Models;

public class PaymentTestRequest
{
    public required string TransactionNo { get; set; }
    public PaymentProvider PaymentProvider { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public decimal RequestAmount { get; set; }
}