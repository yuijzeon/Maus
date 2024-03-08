namespace Maus.Domain.Payment;

public interface IPaymentService
{
    Task PayAsync(PaymentRequest request);
}