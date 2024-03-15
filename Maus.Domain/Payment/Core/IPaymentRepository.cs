namespace Maus.Domain.Payment.Core;

public interface IPaymentRepository
{
    PaymentChannel GetPaymentChannel(PaymentProvider paymentProvider);
}