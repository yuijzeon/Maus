using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Domain.Payment.EcPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Web.Controllers;

[ApiController]
[Route("ec-pay")]
public class EcPayController(IEcPayProxy ecPayProxy, IPaymentRepository paymentRepository) : Controller
{
    [HttpPost("callback")]
    public async Task<IActionResult> Callback([FromForm] EcPayDepositCallback request)
    {
        var paymentChannel = await paymentRepository.GetPaymentChannel(ProviderCode.EcPay);
        request.CheckSignature(paymentChannel.MerchantKey, paymentChannel.MerchantIv);
        var query = await ecPayProxy.Query(request.MerchantTradeNo, paymentChannel);
        return Ok("1|OK");
    }
}