using Maus.Server.Models;
using Maus.Server.Payment;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Controllers;

[ApiController]
[Route("")]
public class HomeController(GetDepositService getGetDepositService) : Controller
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
        return await depositService.Deposit(new Transaction
        {
            TransactionNo = request.TransactionNo,
            CreatedDate = request.CreatedDate,
            RequestAmount = request.RequestAmount
        }, this);
    }
}