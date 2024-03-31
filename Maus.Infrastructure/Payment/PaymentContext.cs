using System.Linq.Expressions;
using Maus.Domain.Payment.Core;
using Maus.Infrastructure.Payment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Maus.Infrastructure.Payment;

public class PaymentContext(DbContextOptions<PaymentContext> options) : DbContext(options)
{
    public DbSet<ProviderEntity> Providers { get; set; } = null!;
    public DbSet<ProviderMethodEntity> ProviderMethods { get; set; } = null!;
    public DbSet<ProviderMethodBankEntity> ProviderMethodBanks { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Conventions.Add(dependencies => new DefaultValueSqlConvention(dependencies));

        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeKindConvention>().HavePrecision(0);
        configurationBuilder.Properties<PaymentType>().HaveConversion(typeof(string)).AreUnicode(false).HaveMaxLength(15);
        configurationBuilder.Properties<CurrencyCode>().HaveConversion(typeof(string)).AreUnicode(false).HaveMaxLength(15);
        configurationBuilder.Properties<MethodCode>().HaveConversion(typeof(string)).AreUnicode(false).HaveMaxLength(15);
        configurationBuilder.Properties<BankCode>().HaveConversion(typeof(string)).AreUnicode(false).HaveMaxLength(15);
        configurationBuilder.Properties<ProviderCode>().HaveConversion(typeof(string)).AreUnicode(false).HaveMaxLength(15);
        configurationBuilder.Properties<PaymentStatus>().HaveConversion(typeof(string)).AreUnicode(false).HaveMaxLength(15);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProviderEntity>(entity =>
        {
            entity.HasAlternateKey(x => x.ProviderCode);

            entity.HasMany(x => x.ProviderMethods)
                .WithOne(x => x.ProviderEntity)
                .HasPrincipalKey(x => x.ProviderCode)
                .HasForeignKey(x => x.ProviderCode);
        });

        modelBuilder.Entity<ProviderMethodEntity>(entity =>
        {
            entity.HasAlternateKey(GetPaymentUnitKey<ProviderMethodEntity>());

            entity.HasMany(x => x.ProviderMethodBanks)
                .WithOne(x => x.ProviderMethod)
                .HasPrincipalKey(GetPaymentUnitKey<ProviderMethodEntity>())
                .HasForeignKey(GetPaymentUnitKey<ProviderMethodBankEntity>());
        });
    }

    private static Expression<Func<T, object?>> GetPaymentUnitKey<T>() where T : PaymentUnitBase<int>
    {
        return x => new { x.Type, x.Currency, x.MethodCode, x.ProviderCode };
    }
}