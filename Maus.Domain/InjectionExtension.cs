using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;
using Microsoft.Extensions.DependencyInjection;

namespace Maus.Domain;

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddTransient<IEcPayService, EcPayService>();

        services.AddTransient<Func<PaymentProvider, IPaymentService>>(x => payment =>
        {
            return payment switch
            {
                PaymentProvider.EcPay => x.GetRequiredService<IEcPayService>(),
                _ => throw new NotImplementedException()
            };
        });

        return services;
    }
}