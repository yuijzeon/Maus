using System.Text.RegularExpressions;
using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public class EcPayService(IPaymentRepository paymentRepository, IEcPayProxy ecPayProxy) : PaymentService, IDepositable
{
    public async Task<IPaymentResult> Deposit(PaymentTransaction transaction)
    {
        var channel = await paymentRepository.GetPaymentChannel(transaction.MerchantCode, new PaymentUnit
        {
            PaymentType = transaction.PaymentType,
            MethodCode = transaction.MethodCode,
            SubMethodCode = transaction.SubMethodCode,
            ProviderCode = transaction.ProviderCode
        });

        var response = await ecPayProxy.Query(transaction.MerchantTransactionNo, channel);

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

    public override string GetTransactionNo(string merchantCode, string merchantTransactionNo)
    {
        var transactionNo =  $"{merchantCode.ToUpper()}x{merchantTransactionNo.ToUpper()}";
        return Regex.Replace(transactionNo, @"[^(0-9a-zA-Z)]", string.Empty);
    }
}