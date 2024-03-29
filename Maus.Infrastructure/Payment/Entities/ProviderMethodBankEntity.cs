using System.ComponentModel.DataAnnotations;
using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

public class ProviderMethodBankEntity : IBaseEntity<int>, IPaymentUnit
{
    public BankCode BankCode { get; set; }

    [StringLength(15)]
    public string? ProviderBankCode { get; set; }

    public PaymentStatus Status { get; set; }

    public ProviderMethodEntity ProviderMethod { internal get; set; } = null!;

    public int Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public PaymentType Type { get; set; }
    public CurrencyCode CurrencyCode { get; set; }
    public MethodCode MethodCode { get; set; }
    public ProviderCode ProviderCode { get; set; }
}