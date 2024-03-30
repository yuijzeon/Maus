using System.Linq.Expressions;
using Maus.Infrastructure.Payment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Maus.Infrastructure.Payment;

public class PaymentContext(DbContextOptions<PaymentContext> options) : DbContext(options)
{
    public DbSet<ProviderEntity> Providers { get; set; } = null!;
    public DbSet<ProviderMethodEntity> ProviderMethods { get; set; } = null!;
    public DbSet<ProviderMethodBankEntity> ProviderMethodBanks { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Conventions.Add(x => new DefaultValueSqlConvention(x.GetRequiredService<ProviderConventionSetBuilderDependencies>()));
        configurationBuilder.Conventions.Add(x => new ConvertStringConvention(x.GetRequiredService<ProviderConventionSetBuilderDependencies>()));
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
        return x => new { x.Type, CurrencyCode = x.Currency, x.MethodCode, x.ProviderCode };
    }
}