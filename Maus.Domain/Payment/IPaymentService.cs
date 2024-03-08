namespace Maus.Domain.Payment;

public interface IPaymentService
{
    Task CreatePayIn(PaymentRequest request);
}