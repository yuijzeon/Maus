using System.Linq.Expressions;
using Maus.Infrastructure.Payment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Maus.Infrastructure.Payment;

public class PaymentContext(DbContextOptions<PaymentContext> options) : DbContext(options)
{
    public DbSet<ProviderEntity> Providers { get; set; } = null!;
    public DbSet<ProviderMethodEntity> ProviderMethods { get; set; } = null!;
    public DbSet<ProviderMethodBankEntity> ProviderMethodBanks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseConvertStringAttribute();

        modelBuilder.Entity<ProviderEntity>(entity =>
        {
            entity.HasAlternateKey(x => x.ProviderCode);
            entity.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasMany(x => x.ProviderMethods)
                .WithOne(x => x.ProviderEntity)
                .HasPrincipalKey(x => x.ProviderCode)
                .HasForeignKey(x => x.ProviderCode);
        });

        modelBuilder.Entity<ProviderMethodEntity>(entity =>
        {
            entity.HasAlternateKey(GetPaymentUnitKey<ProviderMethodEntity>());
            entity.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasMany(x => x.ProviderMethodBanks)
                .WithOne(x => x.ProviderMethod)
                .HasPrincipalKey(GetPaymentUnitKey<ProviderMethodEntity>())
                .HasForeignKey(GetPaymentUnitKey<ProviderMethodBankEntity>());
        });

        modelBuilder.Entity<ProviderMethodBankEntity>(entity =>
        {
            entity.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });
    }

    private static Expression<Func<T, object?>> GetPaymentUnitKey<T>() where T : PaymentUnitBase<int>
    {
        return x => new { x.Type, CurrencyCode = x.Currency, x.MethodCode, x.ProviderCode };
    }
}