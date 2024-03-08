using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;
using Microsoft.Extensions.DependencyInjection;

namespace Maus.Domain;

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddKeyedTransient<IPaymentProxy, EcPayProxy>(nameof(PaymentProvider.EcPay));
        services.AddTransient<IEcPayService, EcPayService>();

        services.AddTransient<Func<PaymentProvider, IPaymentProxy>>(x => payment =>
        {
            return payment switch
            {
                PaymentProvider.EcPay => x.GetRequiredKeyedService<IPaymentProxy>(PaymentProvider.EcPay),
                _ => throw new NotImplementedException()
            };
        });

        return services;
    }
}