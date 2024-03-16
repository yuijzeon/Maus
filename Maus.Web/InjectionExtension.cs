using Maus.Domain.Payment;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Infrastructure.Payment;

namespace Maus.Web;

public delegate IDepositService GetDepositService(ProviderCode serviceProviderCode);

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddTransient<IPaymentDao, PaymentDao>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();

        services.AddHttpClient<IEcPayProxy, EcPayProxy>();
        services.AddKeyedTransient<IDepositService, EcPayDepositService>(ProviderCode.EcPay);

        services.AddTransient<GetDepositService>(x =>
            providerCode => x.GetRequiredKeyedService<IDepositService>(providerCode)
        );

        return services;
    }
}