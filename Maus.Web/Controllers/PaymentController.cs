using Maus.Domain.Payment.Core;
using Maus.Web.Models;
using Maus.Web.Views.Payment;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Web.Controllers;

[ApiController]
[Route("payment")]
public class PaymentController(GetDepositService getDepositService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromForm] PaymentTestRequest request)
    {
        var depositService = getDepositService(request.ProviderCode);

        var paymentResult = await depositService.Deposit(new PaymentTransaction
        {
            MerchantCode = "TEST",
            MerchantTransactionNo = request.TransactionNo,
            ProviderCode = request.ProviderCode,
            MethodCode = request.MethodCode,
            SubMethodCode = request.SubMethodCode,
            RequestAmount = request.RequestAmount,
            CreatedDate = DateTimeOffset.Now,
            TransactionNo = string.Empty,
        });

        return paymentResult.Type switch
        {
            PaymentResultType.Unknown => View("Index"),
            PaymentResultType.FormSubmit => ViewFormSubmit((FormSubmitResult)paymentResult),
            PaymentResultType.PlainHtml => ContentHtml((PlainHtmlResult)paymentResult),
            _ => throw new NotImplementedException()
        };
    }

    private ContentResult ContentHtml(PlainHtmlResult result)
    {
        return Content(result.HtmlString, "text/html");
    }

    private ViewResult ViewFormSubmit(FormSubmitResult result)
    {
        return View("FormSubmitDirectly", new FormSubmitDirectly
        {
            ActionUrl = result.ActionUrl,
            NameValues = result.FormData
        });
    }
}