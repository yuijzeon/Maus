using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

[Table("ProviderMethod")]
public class ProviderMethodEntity : PaymentUnitBase<int>
{
    [Column("ProviderMethodCode")]
    [StringLength(15)]
    public string? ProviderMethodCode { get; set; }

    [Column("Status")]
    [ConvertString(false, 15)]
    public PaymentStatus Status { get; set; }

    public ProviderEntity ProviderEntity { internal get; set; } = null!;
    public ICollection<ProviderMethodBankEntity>? ProviderMethodBanks { get; set; }
}