namespace Maus.Server.Domain.Payment.EcPay;

public interface IEcPayProxy
{
    Task<string> AioCheckOut(PaymentChannel paymentChannel, OrderDetail orderDetail);
}