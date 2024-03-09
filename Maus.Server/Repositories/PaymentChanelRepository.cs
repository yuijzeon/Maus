using Maus.Server.Payment;

namespace Maus.Server.Repositories;

public class PaymentChanelRepository : IPaymentChanelRepository
{
    public PaymentChannel GetPaymentChannel(PaymentProvider ecPay)
    {
        if (ecPay == PaymentProvider.EcPay)
        {
            return new PaymentChannel
            {
                PaymentProvider = PaymentProvider.EcPay,
                MerchantCode = "3002607",
                MerchantKey = "pwFHCqoQZGmho4w6",
                MerchantIv = "EkRm7iFT261dpevs",
                SubmitUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
                QueryUrl = "https://payment-stage.ecpay.com.tw/Cashier/QueryTradeInfo/V5",
                CallbackUrl = "https://3152-2001-b011-4002-1b86-d5ea-b70a-3392-3829.ngrok-free.app/ec-pay/callback"
            };
        }

        return null;
    }
}