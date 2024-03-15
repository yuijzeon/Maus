using Maus.Domain.Payment;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Infrastructure.Payment;

namespace Maus.Web;

public delegate IDepositService GetDepositService(PaymentProvider serviceProvider);

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddTransient<IPaymentRepository, PaymentRepository>();

        services.AddHttpClient<IEcPayProxy, EcPayProxy>();
        services.AddKeyedTransient<IDepositService, EcPayDepositService>(PaymentProvider.EcPay);

        services.AddTransient<GetDepositService>(x =>
            paymentProvider => x.GetRequiredKeyedService<IDepositService>(paymentProvider)
        );

        return services;
    }
}