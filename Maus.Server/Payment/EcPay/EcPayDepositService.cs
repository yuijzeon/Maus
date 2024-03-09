using Maus.Server.Extensions;
using Maus.Server.Payment.EcPay.Models;
using Maus.Server.Views.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment.EcPay;

public class EcPayDepositService : IDepositService
{
    public Task<IActionResult> Deposit(Transaction transaction, Controller controller)
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

        var request = new EcPayDepositRequest(paymentChannel, transaction);
        request.GenerateSignature(paymentChannel.MerchantKey, paymentChannel.MerchantIv);

        var view = controller.View("FormSubmitDirectly", new FormSubmitDirectly
        {
            ActionUrl = paymentChannel.SubmitUrl,
            NameValues = request.ToStringDictionary()
        });

        return Task.FromResult<IActionResult>(view);
    }
}