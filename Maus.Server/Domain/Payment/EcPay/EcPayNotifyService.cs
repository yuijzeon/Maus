using System.Collections;
using ECPay.Payment.Integration;
using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Server.Domain.Payment.EcPay;

public class EcPayNotifyService : IEcPayNotifyService
{
    public Task PayInNotify(EcPayPayInCallback request)
    {
        using (var oPayment = new AllInOne())
        {
            oPayment.HashKey = "5294y06JbISpM5x9"; //ECPay 提供的 HashKey
            oPayment.HashIV = "v77hoKGq4kWxNNIS"; //ECPay 提供的 HashIV
            Hashtable? hashtable = null;
            oPayment.CheckOutFeedback(new SortedDictionary<string, string>(), ref hashtable);
        }

        return Task.CompletedTask;
    }
}