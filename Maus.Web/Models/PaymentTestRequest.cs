using Maus.Domain.Payment.Core;

namespace Maus.Web.Models;

public class PaymentTestRequest
{
    public required string TransactionNo { get; set; }
    public ProviderCode ProviderCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public decimal RequestAmount { get; set; }
}