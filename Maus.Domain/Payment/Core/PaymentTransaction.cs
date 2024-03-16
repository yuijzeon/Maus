namespace Maus.Domain.Payment.Core;

public class PaymentTransaction
{
    public required string TransactionNo { get; set; }
    public ProviderCode ProviderCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public SubMethodCode SubMethodCode { get; set; }
    public decimal RequestAmount { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? ItemName { get; set; }
    public string? Remark { get; set; }
}