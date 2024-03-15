using Maus.Domain.Payment.Core;
using Maus.Server.Models;
using Maus.Server.Views.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Controllers;

[ApiController]
[Route("payment")]
public class PaymentController(GetDepositService getGetDepositService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromForm] PaymentTestRequest request)
    {
        var depositService = getGetDepositService(request.PaymentProvider);

        var paymentResult = await depositService.Deposit(new PaymentTransaction
        {
            TransactionNo = request.TransactionNo,
            PaymentProvider = request.PaymentProvider,
            RequestAmount = request.RequestAmount,
            CreatedDate = DateTimeOffset.Now,
        });

        return paymentResult.Type switch
        {
            PaymentResultType.Unknown => View("Index"),
            PaymentResultType.FormSubmit => ViewFormSubmit((FormSubmitResult)paymentResult),
            _ => throw new NotImplementedException(),
        };
    }

    private ViewResult ViewFormSubmit(FormSubmitResult result)
    {
        return View("FormSubmitDirectly", new FormSubmitDirectly
        {
            ActionUrl = result.ActionUrl,
            NameValues = result.FormData,
        });
    }
}