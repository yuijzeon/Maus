namespace Maus.Domain.Payment.EcPay;

public interface IEcPayService
{
    Task<object> PayInNotify(EcPayPayInCallback request);
}