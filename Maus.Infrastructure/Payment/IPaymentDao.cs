using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public interface IPaymentDao
{
    Task<ProviderConfig> GetProviderConfig(ProviderCode providerCode, MethodCode methodCode, SubMethodCode subMethodCode);
    Task<MerchantProviderConfig> GetMerchantConfig(ProviderCode providerCode);
}