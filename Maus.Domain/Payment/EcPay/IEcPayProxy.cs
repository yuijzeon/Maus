using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public interface IEcPayProxy
{
    Task<object> CreateAioCheckOut(EcPayAioCheckOutRequest ecPayAioCheckOutRequest);
}