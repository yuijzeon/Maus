using Maus.Server.Extensions;
using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;
using Maus.Server.Views;
using Maus.Server.Views.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Maus.Server.Payment.EcPay;

public class EcPayApplyService(IEcPayProxy ecPayProxy) : IOrderApplyService
{
    public Task<IActionResult> CreatePayIn(OrderDetail orderDetail)
    {
        var paymentChannel = new PaymentChannel
        {
            PaymentProvider = PaymentProvider.EcPay,
            MerchantCode = "3002607",
            MerchantKey = "pwFHCqoQZGmho4w6",
            MerchantIv = "EkRm7iFT261dpevs",
            SubmitUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
            CallbackUrl = "https://www.ecpay.com.tw/example/receive"
        };

        var request = new EcPayAllPaymentRequest(paymentChannel, orderDetail);

        var view = new ViewResult
        {
            ViewName = nameof(FormSubmitDirectly),
            ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = new FormSubmitDirectly
                {
                    ActionUrl = paymentChannel.SubmitUrl,
                    NameValues = request.ToStringDictionary()
                }
            }
        };

        return Task.FromResult<IActionResult>(view);
    }
}