using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public class PaymentRepository(IPaymentDao paymentDao) : IPaymentRepository
{
    public async Task<PaymentChannel> GetPaymentChannel(ProviderCode providerCode,
        MethodCode methodCode = MethodCode.Unspecified, SubMethodCode subMethodCode = SubMethodCode.Unspecified)
    {
        var merchantConfig = await paymentDao.GetMerchantConfig(providerCode);
        var providerConfig = await paymentDao.GetProviderConfig(providerCode, methodCode, subMethodCode);

        return new PaymentChannel
        {
            ProviderMerchantCode = merchantConfig.ProviderMerchantCode,
            HashKey = merchantConfig.ProviderMerchantKey,
            HashIv = merchantConfig.ProviderMerchantIv,
            ProviderCode = providerCode,
            MethodCode = methodCode,
            ProviderBankCode = providerConfig.ProviderBankCode,
            ProviderMethodCode = providerConfig.ProviderMethodCode,
            SubmitUrl = providerConfig.UrlConfig.SubmitUrl,
            QueryUrl = providerConfig.UrlConfig.QueryUrl,
            CallbackUrl = providerConfig.UrlConfig.CallbackUrl
        };
    }
}