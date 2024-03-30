using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Maus.Infrastructure.Payment.Entities;

public class EntityBase<TId> where TId : INumber<TId>
{
    [Key]
    [Column("Id")]
    public TId Id { get; set; } = default!;

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [Column("CreatedBy")]
    [StringLength(255)]
    public string CreatedBy { get; set; } = null!;

    [Column("UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [Column("UpdatedBy")]
    [StringLength(255)]
    public string? UpdatedBy { get; set; }

    [Column("IsDeleted")]
    public bool IsDeleted { get; set; }
}