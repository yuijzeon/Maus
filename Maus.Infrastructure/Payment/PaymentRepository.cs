using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public class PaymentRepository : IPaymentRepository
{
    public PaymentChannel GetPaymentChannel(PaymentProvider paymentProvider)
    {
        if (paymentProvider == PaymentProvider.EcPay)
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

        throw new NotImplementedException();
    }
}