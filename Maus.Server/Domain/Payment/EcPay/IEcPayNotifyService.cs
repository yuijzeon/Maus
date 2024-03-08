using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Server.Domain.Payment.EcPay;

public interface IEcPayNotifyService
{
    Task PayInNotify(EcPayPayInCallback request);
}