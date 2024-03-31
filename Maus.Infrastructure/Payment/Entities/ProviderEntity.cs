using System.ComponentModel.DataAnnotations.Schema;
using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

[Table("Provider")]
public class ProviderEntity : EntityBase<int>
{
    [Column("ProviderCode")]
    public ProviderCode ProviderCode { get; set; }

    [Column("Status")]
    public PaymentStatus Status { get; set; }

    public ICollection<ProviderMethodEntity>? ProviderMethods { get; set; }
}