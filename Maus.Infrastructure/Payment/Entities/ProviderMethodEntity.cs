using System.ComponentModel.DataAnnotations;
using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

public class ProviderMethodEntity : IBaseEntity<int>, IPaymentUnit
{
    [StringLength(15)]
    public string? ProviderMethodCode { get; set; }

    public PaymentStatus Status { get; set; }

    public ProviderEntity Provider { internal get; set; } = null!;
    public ICollection<ProviderMethodBankEntity>? ProviderMethodBanks { get; set; }

    public int Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public PaymentType Type { get; set; }
    public CurrencyCode CurrencyCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public ProviderCode ProviderCode { get; set; }
}