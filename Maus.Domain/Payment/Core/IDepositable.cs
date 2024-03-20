namespace Maus.Domain.Payment.Core;

public interface IDepositable
{
    Task<IPaymentResult> Deposit(PaymentTransaction transaction);
}