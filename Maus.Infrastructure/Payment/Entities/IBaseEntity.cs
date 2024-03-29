using System.Numerics;

namespace Maus.Infrastructure.Payment.Entities;

public interface IBaseEntity<TId> where TId : INumber<TId>
{
    TId Id { get; set; }
    DateTime UpdatedAt { get; set; }
    bool IsDeleted { get; set; }
}