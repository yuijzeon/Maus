using Maus.Server.Domain.Payment;
using Maus.Server.Domain.Payment.EcPay;

namespace Maus.Server.Domain;

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