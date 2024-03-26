using Maus.Domain.Payment.Core;

namespace Maus.Domain.Payment.Deposit;

public interface IDepositable
{
    Task<IPaymentResult> Deposit(PaymentTransaction transaction);
}