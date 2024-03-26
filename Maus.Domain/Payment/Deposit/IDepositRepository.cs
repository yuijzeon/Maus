using Maus.Domain.Payment.Core;

namespace Maus.Domain.Payment.Deposit;

public interface IDepositRepository
{
    Task<PaymentChannel> GetPaymentChannel(string merchantCode, PaymentUnit paymentUnit);

    Task<PaymentTransaction> GetTransaction(string transactionNo);
}