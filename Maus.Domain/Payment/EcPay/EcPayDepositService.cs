using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public class EcPayDepositService(IPaymentRepository paymentRepository, IEcPayProxy ecPayProxy) : IDepositService
{
    public async Task<IPaymentResult> Deposit(PaymentTransaction transaction)
    {
        var channel = await paymentRepository.GetPaymentChannel(ProviderCode.EcPay, transaction.MethodCode, transaction.SubMethodCode);

        var response = await ecPayProxy.Query(transaction.TransactionNo, channel);

        if (response.AlreadyCreated())
        {
            throw new PaymentException("Transaction already created");
        }

        var request = new EcPayDepositRequest(transaction, channel);

        return new FormSubmitResult
        {
            ActionUrl = channel.SubmitUrl,
            FormData = request.ToStringDictionary()
        };
    }
}