using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;
using Maus.Domain.Payment.EcPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class EcPayController(
    IEcPayNotifyService ecPayNotifyService,
    [FromKeyedServices(nameof(PaymentProvider.EcPay))]
    IOrderApplyService ecPayApplyService,
    ILogger<EcPayController> logger) : ControllerBase
{
    [HttpPost("test-pay-in")]
    public async Task<object> CreatePayIn()
    {
        return await ecPayApplyService.CreatePayIn(new PaymentRequest());
    }

    [HttpGet("[action]")]
    public async Task<object> PayInNotify(EcPayPayInCallback request)
    {
        return await ecPayNotifyService.PayInNotify(request);
    }
}