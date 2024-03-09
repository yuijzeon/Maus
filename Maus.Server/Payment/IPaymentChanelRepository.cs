namespace Maus.Server.Payment;

public interface IPaymentChanelRepository
{
    PaymentChannel GetPaymentChannel(PaymentProvider ecPay);
}