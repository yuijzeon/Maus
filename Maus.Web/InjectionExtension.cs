﻿using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.Deposit;
using Maus.Domain.Payment.EcPay;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Infrastructure.Payment;

namespace Maus.Web;

public delegate IDepositable GetDepositService(ProviderCode serviceProviderCode);

public static class InjectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddTransient<IPaymentDao, PaymentDao>();
        services.AddTransient<IDepositRepository, DepositRepository>();

        services.AddHttpClient<IEcPayProxy, EcPayProxy>();
        services.AddKeyedTransient<IDepositable, EcPayService>(ProviderCode.EcPay);

        services.AddTransient<GetDepositService>(x =>
            providerCode => x.GetRequiredKeyedService<IDepositable>(providerCode)
        );

        return services;
    }
}