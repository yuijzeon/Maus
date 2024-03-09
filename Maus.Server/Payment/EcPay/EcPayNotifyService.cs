using System.Collections;
using ECPay.Payment.Integration;
using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;

namespace Maus.Server.Payment.EcPay;

public class EcPayNotifyService : IEcPayNotifyService
{
    public Task DepositCallback(EcPayDepositCallback request)
    {
        using (var oPayment = new AllInOne())
        {
            oPayment.HashKey = "5294y06JbISpM5x9"; //ECPay 提供的 HashKey
            oPayment.HashIV = "v77hoKGq4kWxNNIS"; //ECPay 提供的 HashIV
            Hashtable hashtable = null;
            oPayment.CheckOutFeedback(new SortedDictionary<string, string>(), ref hashtable);
        }

        return Task.CompletedTask;
    }
}