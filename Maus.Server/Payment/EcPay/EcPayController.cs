using System.Collections.Specialized;
using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment.EcPay;

[ApiController]
[Route("ec-pay")]
public class EcPayController(
    IEcPayProxy ecPayProxy,
    IPaymentChanelRepository paymentChanelRepository,
    [FromKeyedServices(nameof(PaymentProvider.EcPay))]
    IDepositService ecPayDepositService)
    : Controller
{
    [HttpGet("test")]
    public async Task<IActionResult> CreateTestDeposit()
    {
        var orderDetail = new Transaction
        {
            TransactionNo = "ABBY" + new Random().Next(0, 99999),
            CreatedDate = DateTimeOffset.Now,
            RequestAmount = 3280
        };

        return await ecPayDepositService.Deposit(orderDetail, this);
    }

    [HttpPost("callback")]
    public async Task<IActionResult> Callback([FromForm] EcPayDepositCallback request)
    {
        var paymentChannel = paymentChanelRepository.GetPaymentChannel(PaymentProvider.EcPay);
        request.CheckSignature(paymentChannel.MerchantKey, paymentChannel.MerchantIv);
        var query = await ecPayProxy.Query(request.MerchantTradeNo, paymentChannel);
        return Ok("1|OK");
    }
    
    [HttpGet("query")]
    public async Task<EcPayQueryResponse> Query(string transactionNo)
    {
        var paymentChannel = paymentChanelRepository.GetPaymentChannel(PaymentProvider.EcPay);
        var query = await ecPayProxy.Query(transactionNo, paymentChannel);
        return query;
    }
}