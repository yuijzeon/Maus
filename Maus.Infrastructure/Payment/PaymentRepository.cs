﻿using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public class PaymentRepository(IPaymentDao paymentDao) : IPaymentRepository
{
    public async Task<PaymentChannel> GetPaymentChannel(ProviderCode providerCode,
        MethodCode methodCode = MethodCode.Unspecified)
    {
        var providerConfig = await paymentDao.GetProviderConfig(providerCode, methodCode);
        var merchantConfig = await paymentDao.GetMerchantConfig(providerCode, methodCode);

        return new PaymentChannel
        {
            ProviderCode = providerCode,
            MethodCode = methodCode,
            ProviderMethodCode = providerConfig.ProviderMethodCode,
            ProviderMerchantCode = merchantConfig.ProviderMerchantCode,
            HashKey = merchantConfig.ProviderMerchantKey,
            HashIv = merchantConfig.ProviderMerchantIv,
            SubmitUrl = providerConfig.SubmitUrl,
            QueryUrl = providerConfig.QueryUrl,
            CallbackUrl = providerConfig.CallbackUrl,
        };
    }
}