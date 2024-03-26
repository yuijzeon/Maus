namespace Maus.Domain.Payment.Core;

public class PaymentTransaction
{
    public required string MerchantCode { get; set; }
    public required string MerchantTransactionNo { get; set; }
    public PaymentType PaymentType { get; set; }
    public MethodCode MethodCode { get; set; }
    public BankCode BankCode { get; set; }
    public ProviderCode ProviderCode { get; set; }
    public required string TransactionNo { get; set; }
    public decimal RequestAmount { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? ItemName { get; set; }
    public string? Remark { get; set; }
}