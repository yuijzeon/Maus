using System.ComponentModel.DataAnnotations.Schema;
using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

[Table("Provider")]
public class ProviderEntity : EntityBase<int>
{
    [Column("ProviderCode")]
    [ConvertString(false, 15)]
    public ProviderCode ProviderCode { get; set; }

    [Column("Status")]
    [ConvertString(false, 15)]
    public PaymentStatus Status { get; set; }

    public ICollection<ProviderMethodEntity>? ProviderMethods { get; set; }
}