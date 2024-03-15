using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay.Interfaces;

public interface IEcPayProxy
{
    Task<EcPayQueryResponse> Query(string transactionNo, PaymentChannel paymentChannel);
}