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
        var transaction = await paymentRepository.GetTransaction(request.MerchantTradeNo);
        var channel = await paymentRepository.GetPaymentChannel(transaction.MerchantCode, new PaymentUnit
        {
            PaymentType = transaction.PaymentType,
            MethodCode = transaction.MethodCode,
            SubMethodCode = transaction.SubMethodCode,
            ProviderCode = transaction.ProviderCode
        });

        request.CheckSignature(channel.HashKey, channel.HashIv);
        var query = await ecPayProxy.Query(request.MerchantTradeNo, channel);
        return Ok("1|OK");
    }
}