using Maus.Domain.Payment.EcPay.Models;
using Maus.Server.Domain.Payment;
using Maus.Server.Domain.Payment.EcPay;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class EcPayController(
    IEcPayNotifyService ecPayNotifyService,
    [FromKeyedServices(nameof(PaymentProvider.EcPay))]
    IOrderApplyService ecPayApplyService,
    ILogger<EcPayController> logger) : ControllerBase
{
    [HttpPost("test-pay-in")]
    public async Task<object?> CreatePayIn()
    {
        var orderDetail = new OrderDetail
        {
            OrderNo = "ABBY" + new Random().Next(0, 99999),
            CreatedDate = DateTimeOffset.Now,
            RequestAmount = 3280,
        };
        
        Content(orderDetail.OrderNo);
        return await ecPayApplyService.CreatePayIn(orderDetail);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> PayInNotify(EcPayPayInCallback request)
    {
        await ecPayNotifyService.PayInNotify(request);
        return Ok("1|OK");
    }
}