using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace Maus.Infrastructure.Payment.Entities;

public class EntityBase<TId> where TId : INumber<TId>
{
    [Key]
    [Column("Id", Order = 0)]
    public TId Id { get; set; } = default!;

    [Column("CreatedAt")]
    [Precision(2)]
    [DefaultValueSql("GETUTCDATE()")]
    public DateTime CreatedAt { get; set; }

    [Column("CreatedBy")]
    [StringLength(255)]
    public string CreatedBy { get; set; } = null!;

    [Column("UpdatedAt")]
    [Precision(2)]
    public DateTime? UpdatedAt { get; set; }

    [Column("UpdatedBy")]
    [StringLength(255)]
    public string? UpdatedBy { get; set; }

    [Column("IsDeleted")]
    public bool IsDeleted { get; set; }
}