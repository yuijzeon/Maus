using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public class PaymentDao : IPaymentDao
{
    public Task<ProviderConfig> GetProviderConfig(ProviderCode providerCode, MethodCode methodCode)
    {
        var providerConfig = ((List<ProviderConfig>)
            [
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.Unspecified,
                    ProviderMethodCode = "ALL",
                    SubmitUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
                    CallbackUrl = "https://243c-1-200-146-209.ngrok-free.app/ec-pay/callback",
                    QueryUrl = "https://payment-stage.ecpay.com.tw/Cashier/QueryTradeInfo/V5",
                }
            ])
            .Where(x => x.ProviderCode == providerCode)
            .Where(x => x.MethodCode == methodCode)
            .Single();

        return Task.FromResult(providerConfig);
    }

    public Task<MerchantProviderConfig> GetMerchantConfig(ProviderCode providerCode, MethodCode methodCode)
    {
        var merchantConfig = ((List<MerchantProviderConfig>)
            [
                new MerchantProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.Unspecified,
                    ProviderMerchantCode = "3002607",
                    ProviderMerchantKey = "pwFHCqoQZGmho4w6",
                    ProviderMerchantIv = "EkRm7iFT261dpevs",
                }
            ])
            .Where(x => x.ProviderCode == providerCode)
            .Where(x => x.MethodCode == methodCode)
            .Single();

        return Task.FromResult(merchantConfig);
    }
}