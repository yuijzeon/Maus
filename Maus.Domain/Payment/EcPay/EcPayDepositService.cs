using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public class EcPayDepositService(IPaymentRepository paymentRepository) : IDepositService
{
    public Task<IPaymentResult> Deposit(PaymentTransaction paymentTransaction)
    {
        var paymentChannel = paymentRepository.GetPaymentChannel(PaymentProvider.EcPay);

        var request = new EcPayDepositRequest(paymentChannel, paymentTransaction);

        return Task.FromResult<IPaymentResult>(new FormSubmitResult
        {
            ActionUrl = paymentChannel.SubmitUrl,
            FormData = request.ToStringDictionary(),
            Type = PaymentResultType.FormSubmit,
        });
    }
}