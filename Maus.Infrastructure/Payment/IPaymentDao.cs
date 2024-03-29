using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public interface IPaymentDao
{
    Task<ProviderConfig> GetProviderConfig(PaymentUnit paymentUnit);
    Task<MerchantConfig> GetMerchantConfig(string merchantCode, ProviderCode providerCode);
}