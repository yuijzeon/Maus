namespace Maus.Server.Payment;

public class Transaction
{
    public required string TransactionNo { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public decimal RequestAmount { get; set; }
}