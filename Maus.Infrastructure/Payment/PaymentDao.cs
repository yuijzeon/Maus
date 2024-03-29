using Maus.Domain.Payment.Core;
using Microsoft.EntityFrameworkCore;

namespace Maus.Infrastructure.Payment;

public class PaymentDao : IPaymentDao
{
    public async Task<ProviderConfig> GetProviderConfig(PaymentUnit paymentUnit)
    {
        var ecPayUrlConfig = new UrlConfig
        {
            SubmitUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
            CallbackUrl = "https://243c-1-200-146-209.ngrok-free.app/ec-pay/callback",
            QueryUrl = "https://payment-stage.ecpay.com.tw/Cashier/QueryTradeInfo/V5"
        };

        var providerConfig = await ((List<ProviderConfig>)
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
                ..((List<(BankCode key, string value)>)
                [
                    (BankCode.Unspecified, "0"),
                    (BankCode.UnionPay, "1"),
                    (BankCode.UnionPayWithout, "2")
                ]).Select(x => new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.CreditCard,
                    BankCode = x.key,
                    ProviderMethodCode = "Credit",
                    ProviderBankCode = x.value,
                    UrlConfig = ecPayUrlConfig
                }).ToList(),
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.EWallet,
                    BankCode = BankCode.ApplePay,
                    ProviderMethodCode = "ApplePay",
                    ProviderBankCode = null,
                    UrlConfig = ecPayUrlConfig
                },
                ..((List<(BankCode key, string value)>)
                [
                    (BankCode.Unspecified, null),
                    (BankCode.TSIB, "TAISHIN"),
                    (BankCode.ESUN, "ESUN"),
                    (BankCode.BKTW, "BOT"),
                    (BankCode.TPBK, "FUBON"),
                    (BankCode.CTCB, "CHINATRUST"),
                    (BankCode.FCBK, "FIRST"),
                    (BankCode.LBOT, "LAND"),
                    (BankCode.UWCB, "CATHAY"),
                    (BankCode.OURB, "TACHONG"),
                    (BankCode.BBBK, "PANHSIN")
                ]).Select(x => new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.BankTransfer,
                    BankCode = x.key,
                    ProviderMethodCode = "ATM",
                    ProviderBankCode = x.value,
                    UrlConfig = ecPayUrlConfig
                }).ToList(),
                ..((List<(BankCode key, string value)>)
                [
                    (BankCode.Unspecified, "CVS"),
                    (BankCode.OkMart, "OK"),
                    (BankCode.FamilyMart, "FAMILY"),
                    (BankCode.HiLife, "HILIFE"),
                    (BankCode.IBon, "IBON")
                ]).Select(x => new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.CvsKiosk,
                    BankCode = x.key,
                    ProviderMethodCode = "CVS",
                    ProviderBankCode = x.value,
                    UrlConfig = ecPayUrlConfig
                }).ToList(),
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.Barcode,
                    BankCode = BankCode.Unspecified,
                    ProviderMethodCode = "BARCODE",
                    ProviderBankCode = "BARCODE",
                    UrlConfig = ecPayUrlConfig
                },
                ..((List<(BankCode key, string value)>)
                [
                    (BankCode.Unspecified, null),
                    (BankCode.TSIB, "TAISHIN"),
                    (BankCode.ESUN, "ESUN"),
                    (BankCode.BKTW, "BOT"),
                    (BankCode.TPBK, "FUBON"),
                    (BankCode.CTCB, "CHINATRUST"),
                    (BankCode.FCBK, "FIRST"),
                    (BankCode.UWCB, "CATHAY"),
                    (BankCode.ICBC, "MEGA"),
                    (BankCode.LBOT, "LAND"),
                    (BankCode.OURB, "TACHONG"),
                    (BankCode.SINO, "SINOPAC")
                ]).Select(x => new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.InternetBanking,
                    BankCode = x.key,
                    ProviderMethodCode = "WebATM",
                    ProviderBankCode = x.value,
                    UrlConfig = ecPayUrlConfig
                }).ToList(),
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.QrCode,
                    BankCode = BankCode.TWQR,
                    ProviderMethodCode = "TWQR",
                    ProviderBankCode = null,
                    UrlConfig = ecPayUrlConfig
                },
                new ProviderConfig
                {
                    ProviderCode = ProviderCode.EcPay,
                    MethodCode = MethodCode.PayLater,
                    BankCode = BankCode.Unspecified,
                    ProviderMethodCode = "BNPL",
                    ProviderBankCode = null,
                    UrlConfig = ecPayUrlConfig,
                }
            ])
            .AsQueryable()
            .Where(x => x.ProviderCode == paymentUnit.ProviderCode)
            .Where(x => x.MethodCode == paymentUnit.MethodCode)
            .Where(x => x.BankCode == paymentUnit.BankCode)
            .SingleAsync();

        return providerConfig;
    }

    public async Task<MerchantConfig> GetMerchantConfig(string merchantCode, ProviderCode providerCode)
    {
        var merchantConfig = await ((List<MerchantConfig>)
            [
                new MerchantConfig
                {
                    MerchantCode = merchantCode,
                    ProviderCode = ProviderCode.EcPay,
                    ProviderMerchantCode = "3002607",
                    ProviderMerchantKey = "pwFHCqoQZGmho4w6",
                    ProviderMerchantIv = "EkRm7iFT261dpevs"
                }
            ])
            .AsQueryable()
            .Where(x => x.MerchantCode == merchantCode)
            .Where(x => x.ProviderCode == providerCode)
            .SingleAsync();

        return merchantConfig;
    }
}