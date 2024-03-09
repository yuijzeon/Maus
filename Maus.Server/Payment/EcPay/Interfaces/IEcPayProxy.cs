namespace Maus.Server.Payment.EcPay.Interfaces;

public interface IEcPayProxy
{
    Task<string> AioCheckOut(PaymentChannel paymentChannel, Transaction transaction);
}