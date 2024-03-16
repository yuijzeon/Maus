namespace Maus.Domain.Payment.Core;

public interface IPaymentRepository
{
    Task<PaymentChannel> GetPaymentChannel(string merchantCode, PaymentUnit paymentUnit);

    Task<PaymentTransaction> GetTransaction(string transactionNo);
}