using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.Deposit;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Domain.Payment.EcPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Web.Controllers;

[ApiController]
[Route("ec-pay")]
public class EcPayController(IEcPayProxy ecPayProxy, IDepositRepository depositRepository) : Controller
{
    [HttpPost("callback")]
    public async Task<IActionResult> Callback([FromForm] EcPayDepositCallback request)
    {
        var transaction = await depositRepository.GetTransaction(request.MerchantTradeNo);
        var channel = await depositRepository.GetPaymentChannel(transaction.MerchantCode, new PaymentUnit
        {
            PaymentType = transaction.PaymentType,
            MethodCode = transaction.MethodCode,
            BankCode = transaction.BankCode,
            ProviderCode = transaction.ProviderCode
        });

        request.CheckSignature(channel.HashKey, channel.HashIv);
        var query = await ecPayProxy.Query(request.MerchantTradeNo, channel);
        return Ok("1|OK");
    }
}