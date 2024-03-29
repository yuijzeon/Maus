using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

public interface IPaymentUnit
{
    PaymentType Type { get; set; }
    CurrencyCode CurrencyCode { get; set; }
    MethodCode MethodCode { get; set; }
    ProviderCode ProviderCode { get; set; }
}