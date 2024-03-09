using Maus.Server.Payment;
using Maus.Server.Payment.EcPay;
using Microsoft.AspNetCore.Mvc;

namespace Maus.Test;

[TestClass]
public class EcPayDepositServiceTests
{
    private IDepositService _ecPayDepositService = null!;
    private Controller _controller = null!;

    [TestInitialize]
    public void Initialize()
    {
        _ecPayDepositService = new EcPayDepositService();
        _controller = new EmptyController();
    }

    [TestMethod]
    public async Task ec_pay_create_order()
    {
        var request = new OrderDetail
        {
            OrderNo = "ABBY" + new Random().Next(0, 99999),
            CreatedDate = DateTimeOffset.Now,
            RequestAmount = 3280
        };
        
        var action = async () => await _ecPayDepositService.Deposit(request, _controller);
        await action.Should().NotThrowAsync();
    }
}