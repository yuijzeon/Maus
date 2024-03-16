using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public class PaymentDao : IPaymentDao
{
    public Task<ProviderConfig> GetProviderConfig(ProviderCode providerCode, MethodCode methodCode, BankCode bankCode)
    {
        var ecPayUrlConfig = new UrlConfig
        {
            SubmitUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
            CallbackUrl = "https://243c-1-200-146-209.ngrok-free.app/ec-pay/callback",
            QueryUrl = "https://payment-stage.ecpay.com.tw/Cashier/QueryTradeInfo/V5"
        };

        var providerConfig = ((List<ProviderConfig>)
            [
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.Unspecified,
                    BankCode = BankCode.Unspecified,
                    ProviderMethodCode = "ALL",
                    ProviderBankCode = null,
                    UrlConfig = ecPayUrlConfig
                },
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.CreditCard,
                    BankCode = BankCode.Unspecified,
                    ProviderMethodCode = "Credit",
                    ProviderBankCode = "0",
                    UrlConfig = ecPayUrlConfig
                },
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.CreditCard,
                    BankCode = BankCode.UnionPay,
                    ProviderMethodCode = "Credit",
                    ProviderBankCode = "1",
                    UrlConfig = ecPayUrlConfig
                },
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.CreditCard,
                    BankCode = BankCode.UnionPayWithout,
                    ProviderMethodCode = "Credit",
                    ProviderBankCode = "2",
                    UrlConfig = ecPayUrlConfig
                }
            ])
            .Where(x => x.ProviderCode == providerCode)
            .Where(x => x.MethodCode == methodCode)
            .Where(x => x.BankCode == bankCode)
            .Single();

        return Task.FromResult(providerConfig);
    }

    public Task<MerchantProviderConfig> GetMerchantConfig(ProviderCode providerCode)
    {
        var merchantConfig = ((List<MerchantProviderConfig>)
            [
                new MerchantProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    ProviderMerchantCode = "3002607",
                    ProviderMerchantKey = "pwFHCqoQZGmho4w6",
                    ProviderMerchantIv = "EkRm7iFT261dpevs"
                }
            ])
            .Where(x => x.ProviderCode == providerCode)
            .Single();

        return Task.FromResult(merchantConfig);
    }
}