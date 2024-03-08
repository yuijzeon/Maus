using Maus.Server.Payment.EcPay.Models;

namespace Maus.Server.Payment.EcPay.Interfaces;

public interface IEcPayNotifyService
{
    Task PayInNotify(EcPayPayInCallback request);
}