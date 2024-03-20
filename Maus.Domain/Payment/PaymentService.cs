namespace Maus.Domain.Payment;

public class PaymentService
{
    public virtual string GetTransactionNo(string merchantCode, string merchantTransactionNo)
    {
        return $"{merchantCode}-{merchantTransactionNo}";
    }
}