namespace Maus.Domain.Payment.EcPay;

public interface IEcPayService : IPaymentService
{
    Task PayInCallback();
}