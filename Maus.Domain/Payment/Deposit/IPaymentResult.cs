namespace Maus.Domain.Payment.Deposit;

public interface IPaymentResult
{
    PaymentResultType Type { get; }
}