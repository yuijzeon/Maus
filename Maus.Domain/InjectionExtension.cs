using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;
using Microsoft.Extensions.DependencyInjection;

namespace Maus.Domain;

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddKeyedTransient<IPaymentProxy, EcPayProxy>(nameof(PaymentProvider.EcPay));
        services.AddTransient<IEcPayService, EcPayService>();

        return services;
    }
}