﻿using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

class EcPayProxy : IEcPayProxy
{
    public Task<object> CreateAioCheckOut(EcPayAioCheckOutRequest ecPayAioCheckOutRequest)
    {
        throw new NotImplementedException();
    }
}