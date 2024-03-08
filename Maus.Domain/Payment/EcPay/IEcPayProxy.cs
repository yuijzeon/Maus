namespace Maus.Domain.Payment.EcPay;

public interface IEcPayProxy
{
    Task<object?> AioCheckOut(PaymentRequest request);
}