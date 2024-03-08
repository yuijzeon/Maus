namespace Maus.Server.Payment;

public class OrderDetail
{
    public required string OrderNo { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public decimal RequestAmount { get; set; }
}