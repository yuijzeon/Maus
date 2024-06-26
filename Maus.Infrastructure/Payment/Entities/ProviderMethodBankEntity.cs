﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Maus.Domain.Payment.Core;

namespace Maus.Infrastructure.Payment.Entities;

[Table("ProviderMethodBank")]
public class ProviderMethodBankEntity : PaymentUnitBase<int>
{
    [Column("BankCode")]
    public BankCode BankCode { get; set; }

    [Column("ProviderBankCode")]
    [StringLength(15)]
    public string? ProviderBankCode { get; set; }

    [Column("Status")]
    public PaymentStatus Status { get; set; }

    public ProviderMethodEntity ProviderMethod { internal get; set; } = null!;
}