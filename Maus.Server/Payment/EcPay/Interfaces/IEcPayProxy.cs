using System.Collections.Specialized;
using Maus.Server.Payment.EcPay.Models;

namespace Maus.Server.Payment.EcPay.Interfaces;

public interface IEcPayProxy
{
    Task<EcPayQueryResponse> Query(string transactionNo, PaymentChannel paymentChannel);
}