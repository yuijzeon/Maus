namespace Maus.Domain.Payment;

public interface IPaymentProvider
{
    Task PayAsync(PaymentRequest request);
}