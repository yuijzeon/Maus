using Maus.Domain.Payment.Core;

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
        throw new NotImplementedException();
    }
}