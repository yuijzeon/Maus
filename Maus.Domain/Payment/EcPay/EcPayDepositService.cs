using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public class EcPayDepositService(IPaymentRepository paymentRepository, IEcPayProxy ecPayProxy) : IDepositService
{
    public async Task<IPaymentResult> Deposit(PaymentTransaction transaction)
    {
        var paymentChannel = await paymentRepository.GetPaymentChannel(ProviderCode.EcPay);

        var response = await ecPayProxy.Query(transaction.TransactionNo, paymentChannel);

        var request = new EcPayDepositRequest(paymentChannel, transaction);

        return new FormSubmitResult
        {
            ActionUrl = paymentChannel.SubmitUrl,
            FormData = request.ToStringDictionary(),
            Type = PaymentResultType.FormSubmit,
        };
    }
}