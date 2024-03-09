using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment.EcPay;

[ApiController]
[Route("ec-pay")]
public class EcPayController(IEcPayProxy ecPayProxy, IPaymentChanelRepository paymentChanelRepository) : Controller
{
    [HttpPost("callback")]
    public async Task<IActionResult> Callback([FromForm] EcPayDepositCallback request)
    {
        var paymentChannel = paymentChanelRepository.GetPaymentChannel(PaymentProvider.EcPay);
        request.CheckSignature(paymentChannel.MerchantKey, paymentChannel.MerchantIv);
        var query = await ecPayProxy.Query(request.MerchantTradeNo, paymentChannel);
        return Ok("1|OK");
    }
}