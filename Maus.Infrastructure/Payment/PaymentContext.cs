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
        modelBuilder.Entity<ProviderEntity>(entity =>
        {
            entity.ToTable(GetTableName<ProviderEntity>());
            entity.HasKey(x => x.Id);
            entity.HasAlternateKey(x => x.ProviderCode);

            entity.PropertyAsString15(x => x.ProviderCode);
            entity.PropertyAsString15(x => x.Status);
            entity.Property(x => x.UpdatedAt).HasDefaultValueSql("GETDATE()");

            entity.HasMany(x => x.ProviderMethods)
                .WithOne(x => x.Provider)
                .HasPrincipalKey(x => x.ProviderCode)
                .HasForeignKey(x => x.ProviderCode);
        });

        modelBuilder.Entity<ProviderMethodEntity>(entity =>
        {
            entity.ToTable(GetTableName<ProviderMethodEntity>());
            entity.HasKey(x => x.Id);
            entity.HasAlternateKey(GetPaymentUnitKey<ProviderMethodEntity>());
            entity.PropertyAsString15(x => x.Type);
            entity.PropertyAsString15(x => x.CurrencyCode);
            entity.PropertyAsString15(x => x.MethodCode);
            entity.PropertyAsString15(x => x.ProviderCode);
            entity.PropertyAsString15(x => x.Status);
            entity.Property(x => x.UpdatedAt).HasDefaultValueSql("GETDATE()");

            entity.HasMany(x => x.ProviderMethodBanks)
                .WithOne(x => x.ProviderMethod)
                .HasPrincipalKey(GetPaymentUnitKey<ProviderMethodEntity>())
                .HasForeignKey(GetPaymentUnitKey<ProviderMethodBankEntity>());
        });

        modelBuilder.Entity<ProviderMethodBankEntity>(entity =>
        {
            entity.ToTable(GetTableName<ProviderMethodBankEntity>());
            entity.HasKey(x => x.Id);
            entity.PropertyAsString15(x => x.Type);
            entity.PropertyAsString15(x => x.CurrencyCode);
            entity.PropertyAsString15(x => x.MethodCode);
            entity.PropertyAsString15(x => x.ProviderCode);
            entity.PropertyAsString15(x => x.BankCode);
            entity.PropertyAsString15(x => x.Status);
            entity.Property(x => x.UpdatedAt).HasDefaultValueSql("GETDATE()");
        });
    }

    private static string GetTableName<T>()
    {
        return typeof(T).Name.Replace("Entity", string.Empty);
    }

    private static Expression<Func<T, object?>> GetPaymentUnitKey<T>() where T : class, IPaymentUnit
    {
        return x => new { x.Type, x.CurrencyCode, x.MethodCode, x.ProviderCode };
    }
}