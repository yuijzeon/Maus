namespace Maus.Domain.Payment.EcPay;

public class EcPayApplyService(IEcPayProxy ecPayProxy) : IOrderApplyService
{
    public async Task<object?> CreatePayIn(PaymentRequest request)
    {
        return await ecPayProxy.AioCheckOut(request);
    }
}