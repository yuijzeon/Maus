using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public class EcPayNotifyService : IEcPayNotifyService
{
    public Task<object> PayInNotify(EcPayPayInCallback request)
    {
        throw new NotImplementedException();
    }
}