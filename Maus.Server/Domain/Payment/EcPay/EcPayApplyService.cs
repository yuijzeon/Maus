using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Domain.Payment.EcPay;

public class EcPayApplyService(IEcPayProxy ecPayProxy) : IOrderApplyService
{
    public async Task<IActionResult> CreatePayIn(OrderDetail orderDetail)
    {
        var paymentChannel = new PaymentChannel
        {
            PaymentProvider = PaymentProvider.EcPay,
            MerchantCode = "3002607",
            MerchantKey = "pwFHCqoQZGmho4w6",
            MerchantHashIv = "EkRm7iFT261dpevs",
            SubmitUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
            CallbackUrl = "https://www.ecpay.com.tw/example/receive"
        };

        return new ContentResult
        {
            Content = await ecPayProxy.AioCheckOut(paymentChannel, orderDetail),
            ContentType = "text/html"
        };
    }
}