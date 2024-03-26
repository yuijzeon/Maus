﻿using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public class PaymentRepository(IPaymentDao paymentDao) : IPaymentRepository
{
    public async Task<PaymentChannel> GetPaymentChannel(string merchantCode, PaymentUnit paymentUnit)
    {
        var merchantConfig = await paymentDao.GetMerchantConfig(merchantCode, paymentUnit.ProviderCode);
        var providerConfig = await paymentDao.GetProviderConfig(paymentUnit);

        return new PaymentChannel
        {
            ProviderMerchantCode = merchantConfig.ProviderMerchantCode,
            HashKey = merchantConfig.ProviderMerchantKey,
            HashIv = merchantConfig.ProviderMerchantIv,
            ProviderCode = paymentUnit.ProviderCode,
            MethodCode = paymentUnit.MethodCode,
            ProviderBankCode = providerConfig.ProviderBankCode,
            ProviderMethodCode = providerConfig.ProviderMethodCode,
            SubmitUrl = providerConfig.UrlConfig.SubmitUrl,
            QueryUrl = providerConfig.UrlConfig.QueryUrl,
            CallbackUrl = providerConfig.UrlConfig.CallbackUrl
        };
    }

    public Task<PaymentTransaction> GetTransaction(string transactionNo)
    {
        return Task.FromResult(new PaymentTransaction
        {
            MerchantCode = "TEST",
            MerchantTransactionNo = transactionNo,
            PaymentType = PaymentType.Unspecified,
            MethodCode = MethodCode.Unspecified,
            BankCode = BankCode.Unspecified,
            ProviderCode = ProviderCode.Unspecified,
            TransactionNo = null,
            RequestAmount = 0,
            CreatedDate = default,
            ItemName = null,
            Remark = null
        });
    }
}