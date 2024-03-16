namespace Maus.Domain.Payment.Core;

public interface IPaymentRepository
{
    Task<PaymentChannel> GetPaymentChannel(ProviderCode providerCode, MethodCode methodCode = MethodCode.Unspecified,
        BankCode bankCode = BankCode.Unspecified);
}