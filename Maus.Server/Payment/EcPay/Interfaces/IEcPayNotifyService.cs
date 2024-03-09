using Maus.Server.Payment.EcPay.Models;

namespace Maus.Server.Payment.EcPay.Interfaces;

public interface IEcPayNotifyService
{
    Task DepositCallback(EcPayDepositCallback request);
}