using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public class PaymentDao : IPaymentDao
{
    public Task<ProviderConfig> GetProviderConfig(PaymentUnit paymentUnit)
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
                    SubMethodCode = SubMethodCode.Unspecified,
                    ProviderMethodCode = "ALL",
                    ProviderBankCode = null,
                    UrlConfig = ecPayUrlConfig
                },
                ..((List<(SubMethodCode key, string value)>)
                [
                    (SubMethodCode.Unspecified, "0"),
                    (SubMethodCode.UnionPay, "1"),
                    (SubMethodCode.UnionPayWithout, "2")
                ]).Select(x => new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.CreditCard,
                    SubMethodCode = x.key,
                    ProviderMethodCode = "Credit",
                    ProviderBankCode = x.value,
                    UrlConfig = ecPayUrlConfig
                }).ToList(),
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.QrCode,
                    SubMethodCode = SubMethodCode.ApplePay,
                    ProviderMethodCode = "ApplePay",
                    ProviderBankCode = null,
                    UrlConfig = ecPayUrlConfig
                },
                ..((List<(SubMethodCode key, string value)>)
                [
                    (SubMethodCode.Unspecified, null),
                    (SubMethodCode.TSIB, "TAISHIN"),
                    (SubMethodCode.ESUN, "ESUN"),
                    (SubMethodCode.BKTW, "BOT"),
                    (SubMethodCode.TPBK, "FUBON"),
                    (SubMethodCode.CTCB, "CHINATRUST"),
                    (SubMethodCode.FCBK, "FIRST"),
                    (SubMethodCode.LBOT, "LAND"),
                    (SubMethodCode.UWCB, "CATHAY"),
                    (SubMethodCode.OURB, "TACHONG"),
                    (SubMethodCode.BBBK, "PANHSIN")
                ]).Select(x => new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.BankTransfer,
                    SubMethodCode = x.key,
                    ProviderMethodCode = "ATM",
                    ProviderBankCode = x.value,
                    UrlConfig = ecPayUrlConfig
                }).ToList(),
                ..((List<(SubMethodCode key, string value)>)
                [
                    (SubMethodCode.Unspecified, "CVS"),
                    (SubMethodCode.OkMart, "OK"),
                    (SubMethodCode.FamilyMart, "FAMILY"),
                    (SubMethodCode.HiLife, "HILIFE"),
                    (SubMethodCode.Ibon, "IBON")
                ]).Select(x => new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.Kiosk,
                    SubMethodCode = x.key,
                    ProviderMethodCode = "CVS",
                    ProviderBankCode = x.value,
                    UrlConfig = ecPayUrlConfig
                }).ToList(),
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.Barcode,
                    SubMethodCode = SubMethodCode.Unspecified,
                    ProviderMethodCode = "BARCODE",
                    ProviderBankCode = "BARCODE",
                    UrlConfig = ecPayUrlConfig
                },
                ..((List<(SubMethodCode key, string value)>)
                [
                    (SubMethodCode.Unspecified, null),
                    (SubMethodCode.TSIB, "TAISHIN"),
                    (SubMethodCode.ESUN, "ESUN"),
                    (SubMethodCode.BKTW, "BOT"),
                    (SubMethodCode.TPBK, "FUBON"),
                    (SubMethodCode.CTCB, "CHINATRUST"),
                    (SubMethodCode.FCBK, "FIRST"),
                    (SubMethodCode.UWCB, "CATHAY"),
                    (SubMethodCode.ICBC, "MEGA"),
                    (SubMethodCode.LBOT, "LAND"),
                    (SubMethodCode.OURB, "TACHONG"),
                    (SubMethodCode.SINO, "SINOPAC")
                ]).Select(x => new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.InternetBanking,
                    SubMethodCode = x.key,
                    ProviderMethodCode = "WebATM",
                    ProviderBankCode = x.value,
                    UrlConfig = ecPayUrlConfig
                }).ToList(),
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.QrCode,
                    SubMethodCode = SubMethodCode.TWQR,
                    ProviderMethodCode = "TWQR",
                    ProviderBankCode = null,
                    UrlConfig = ecPayUrlConfig
                },
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.PayLater,
                    SubMethodCode = SubMethodCode.Unspecified,
                    ProviderMethodCode = "BNPL",
                    ProviderBankCode = null,
                    UrlConfig = ecPayUrlConfig
                }
            ])
            .Where(x => x.ProviderCode == paymentUnit.ProviderCode)
            .Where(x => x.MethodCode == paymentUnit.MethodCode)
            .Where(x => x.SubMethodCode == paymentUnit.SubMethodCode)
            .Single();

        return Task.FromResult(providerConfig);
    }

    public Task<MerchantProviderConfig> GetMerchantConfig(string merchantCode, ProviderCode providerCode)
    {
        var merchantConfig = ((List<MerchantProviderConfig>)
            [
                new MerchantProviderConfig
                {
                    MerchantCode = merchantCode,
                    ProviderCode = ProviderCode.EcPay,
                    ProviderMerchantCode = "3002607",
                    ProviderMerchantKey = "pwFHCqoQZGmho4w6",
                    ProviderMerchantIv = "EkRm7iFT261dpevs"
                }
            ])
            .Where(x => x.MerchantCode == merchantCode)
            .Where(x => x.ProviderCode == providerCode)
            .Single();

        return Task.FromResult(merchantConfig);
    }
}