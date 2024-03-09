using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment.EcPay;

[ApiController]
[Route("ec-pay")]
public class EcPayController(
    IEcPayNotifyService ecPayNotifyService,
    [FromKeyedServices(nameof(PaymentProvider.EcPay))]
    IDepositService ecPayDepositService)
    : Controller
{
    [HttpGet("test")]
    public async Task<IActionResult> CreateTestDeposit()
    {
        var orderDetail = new OrderDetail
        {
            OrderNo = "ABBY" + new Random().Next(0, 99999),
            CreatedDate = DateTimeOffset.Now,
            RequestAmount = 3280
        };

        return await ecPayDepositService.Deposit(orderDetail, this);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(EcPayPayInCallback request)
    {
        await ecPayNotifyService.DepositCallback(request);
        return Ok("1|OK");
    }
}