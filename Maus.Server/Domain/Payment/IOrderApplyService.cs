using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Domain.Payment;

public interface IOrderApplyService
{
    Task<IActionResult> CreatePayIn(OrderDetail orderDetail);
}