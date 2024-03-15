namespace Maus.Domain.Payment.Core;

public interface IDepositService
{
    Task<IPaymentResult> Deposit(PaymentTransaction paymentTransaction);
}