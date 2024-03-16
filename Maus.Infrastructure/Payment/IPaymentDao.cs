using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public interface IPaymentDao
{
    Task<ProviderConfig> GetProviderConfig(PaymentUnit paymentUnit);
    Task<MerchantProviderConfig> GetMerchantConfig(string merchantCode, ProviderCode providerCode);
}