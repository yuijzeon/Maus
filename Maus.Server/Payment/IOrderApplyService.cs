using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment;

public interface IOrderApplyService
{
    Task<IActionResult> CreatePayIn(OrderDetail orderDetail);
}