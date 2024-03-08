using Maus.Server.Payment;
using Maus.Server.Payment.EcPay;
using Maus.Server.Payment.EcPay.Interfaces;

namespace Maus.Server;

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