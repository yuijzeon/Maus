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
        var channel = await paymentRepository.GetPaymentChannel(ProviderCode.EcPay);
        request.CheckSignature(channel.HashKey, channel.HashIv);
        var query = await ecPayProxy.Query(request.MerchantTradeNo, channel);
        return Ok("1|OK");
    }
}