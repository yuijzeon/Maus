namespace Maus.Domain.Payment;

public interface IPaymentProxy
{
    Task<object> CreatePayIn(PaymentRequest request);
}