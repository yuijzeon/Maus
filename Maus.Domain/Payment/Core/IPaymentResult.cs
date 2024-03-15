namespace Maus.Domain.Payment.Core;

public interface IPaymentResult
{
    PaymentResultType Type { get; set; }
}