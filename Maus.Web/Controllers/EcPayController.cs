using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class EcPayController(IEcPayService ecPayService, ILogger<EcPayController> logger) : ControllerBase
{

    [HttpGet("[action]")]
    public async Task PayInCallback()
    {
        await ecPayService.PayInCallback();
    }
}