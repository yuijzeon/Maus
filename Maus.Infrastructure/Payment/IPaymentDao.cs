using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment;

public interface IPaymentDao
{
    Task<ProviderConfig> GetProviderConfig(ProviderCode providerCode, MethodCode methodCode, BankCode bankCode);
    Task<MerchantProviderConfig> GetMerchantConfig(ProviderCode providerCode);
}