using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;
using Microsoft.Extensions.DependencyInjection;

namespace Maus.Domain;

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddHttpClient<IEcPayProxy, EcPayProxy>();
        services.AddKeyedTransient<IOrderApplyService, EcPayApplyService>(nameof(PaymentProvider.EcPay));
        services.AddTransient<IEcPayNotifyService, EcPayNotifyService>();

        return services;
    }
}