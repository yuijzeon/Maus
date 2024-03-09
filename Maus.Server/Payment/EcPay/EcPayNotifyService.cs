using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;

namespace Maus.Server.Payment.EcPay;

public class EcPayNotifyService : IEcPayNotifyService
{
    public Task DepositCallback(EcPayDepositCallback request)
    {
        var paymentChannel = new PaymentChannel
        {
            PaymentProvider = PaymentProvider.EcPay,
            MerchantCode = "3002607",
            MerchantKey = "pwFHCqoQZGmho4w6",
            MerchantIv = "EkRm7iFT261dpevs",
            SubmitUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
            CallbackUrl = "https://3152-2001-b011-4002-1b86-d5ea-b70a-3392-3829.ngrok-free.app/ec-pay/callback"
        };
        
        request.CheckSignature(paymentChannel.MerchantKey, paymentChannel.MerchantIv);

        return Task.CompletedTask;
    }
}