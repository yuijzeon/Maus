using Maus.Server.Payment;
using Maus.Server.Payment.EcPay;
using Maus.Server.Payment.EcPay.Interfaces;

namespace Maus.Server;

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddHttpClient<IEcPayProxy, EcPayProxy>();
        services.AddKeyedTransient<IDepositService, EcPayDepositService>(nameof(PaymentProvider.EcPay));
        services.AddTransient<IEcPayNotifyService, EcPayNotifyService>();

        services.AddTransient<Func<PaymentProvider, IDepositService>>(x => key =>
        {
            return key switch
            {
                PaymentProvider.EcPay => x.GetRequiredKeyedService<IDepositService>(nameof(PaymentProvider.EcPay)),
                _ => throw new NotSupportedException()
            };
        });

        return services;
    }
}