using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public interface IEcPayNotifyService
{
    Task<object> PayInNotify(EcPayPayInCallback request);
}