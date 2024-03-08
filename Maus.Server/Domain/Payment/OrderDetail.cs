namespace Maus.Server.Domain.Payment;

public class OrderDetail
{
    public required string OrderNo { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public decimal RequestAmount { get; set; }
}