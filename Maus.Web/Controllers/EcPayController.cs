using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class EcPayController(
    IEcPayService ecPayService,
    [FromKeyedServices(nameof(PaymentProvider.EcPay))]
    IPaymentProxy paymentProxy,
    ILogger<EcPayController> logger) : ControllerBase
{
    [HttpPost("test-pay-in")]
    public async Task<object> CreatePayIn()
    {
        return await paymentProxy.CreatePayIn(new PaymentRequest());
    }

    [HttpGet("[action]")]
    public async Task<object> PayInNotify(EcPayPayInCallback request)
    {
        return await ecPayService.PayInNotify(request);
    }
}