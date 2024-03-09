using Maus.Server.Extensions;
using Maus.Server.Payment.EcPay.Models;
using Maus.Server.Views.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment.EcPay;

public class EcPayDepositService(IPaymentChanelRepository paymentChanelRepository) : IDepositService
{
    public Task<IActionResult> Deposit(Transaction transaction, Controller controller)
    {
        var paymentChannel = paymentChanelRepository.GetPaymentChannel(PaymentProvider.EcPay);

        var request = new EcPayDepositRequest(paymentChannel, transaction);

        var view = controller.View("FormSubmitDirectly", new FormSubmitDirectly
        {
            ActionUrl = paymentChannel.SubmitUrl,
            NameValues = request.ToStringDictionary()
        });

        return Task.FromResult<IActionResult>(view);
    }
}