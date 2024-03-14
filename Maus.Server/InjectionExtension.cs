using Maus.Server.Payment;
using Maus.Server.Payment.EcPay;
using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Repositories;

namespace Maus.Server;

public delegate IDepositService GetDepositService(PaymentProvider serviceProvider);

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddTransient<IPaymentChanelRepository, PaymentChanelRepository>();

        services.AddHttpClient<IEcPayProxy, EcPayProxy>();
        services.AddKeyedTransient<IDepositService, EcPayDepositService>(PaymentProvider.EcPay);

        services.AddTransient<GetDepositService>(x =>
            paymentProvider => x.GetRequiredKeyedService<IDepositService>(paymentProvider)
        );

        return services;
    }
}