using Microsoft.AspNetCore.Mvc;

namespace Maus.Server.Payment;

public interface IDepositService
{
    Task<IActionResult> Deposit(OrderDetail orderDetail, Controller controller);
}