using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

public class ProviderEntity : IBaseEntity<int>
{
    public ProviderCode ProviderCode { get; set; }

    public PaymentStatus Status { get; set; }

    public ICollection<ProviderMethodEntity>? ProviderMethods { get; set; }

    public int Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}