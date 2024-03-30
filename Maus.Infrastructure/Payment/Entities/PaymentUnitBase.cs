using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

public class PaymentUnitBase<TId> : EntityBase<int> where TId : INumber<TId>
{
    [Column("Type", Order = 1)]
    [ConvertString(false, 15)]
    public PaymentType Type { get; set; }

    [Column("Currency", Order = 2)]
    [ConvertString(false, 15)]
    public CurrencyCode Currency { get; set; }

    [Column("MethodCode", Order = 3)]
    [ConvertString(false, 15)]
    public MethodCode MethodCode { get; set; }

    [Column("ProviderCode", Order = 4)]
    [ConvertString(false, 15)]
    public ProviderCode ProviderCode { get; set; }
}