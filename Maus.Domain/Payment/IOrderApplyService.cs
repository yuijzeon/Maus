namespace Maus.Domain.Payment;

public interface IOrderApplyService
{
    Task<object> CreatePayIn(PaymentRequest request);
}