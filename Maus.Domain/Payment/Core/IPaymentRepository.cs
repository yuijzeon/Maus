namespace Maus.Domain.Payment.Core;

public interface IPaymentRepository
{
    Task<PaymentChannel> GetPaymentChannel(ProviderCode providerCode, MethodCode methodCode = MethodCode.Unspecified,
        SubMethodCode subMethodCode = SubMethodCode.Unspecified);
}