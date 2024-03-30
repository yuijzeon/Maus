using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

public class PaymentUnitBase<TId> : EntityBase<int> where TId : INumber<TId>
{
    [Column("Type")]
    [ConvertString(false, 15)]
    public PaymentType Type { get; set; }

    [Column("CurrencyCode")]
    [ConvertString(false, 15)]
    public CurrencyCode CurrencyCode { get; set; }

    [Column("MethodCode")]
    [ConvertString(false, 15)]
    public MethodCode MethodCode { get; set; }

    [Column("ProviderCode")]
    [ConvertString(false, 15)]
    public ProviderCode ProviderCode { get; set; }
}