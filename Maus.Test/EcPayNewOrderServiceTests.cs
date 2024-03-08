using Maus.Server.Payment;
using Maus.Server.Payment.EcPay;
using Maus.Server.Payment.EcPay.Interfaces;

namespace Maus.Test;

[TestClass]
public class EcPayApplyServiceTests
{
    private IOrderApplyService _ecPayApplyService = null!;

    [TestInitialize]
    public void Initialize()
    {
        var ecPayProxy = Substitute.For<IEcPayProxy>();
        _ecPayApplyService = new EcPayApplyService(ecPayProxy);
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

        var action = async () => await _ecPayApplyService.CreatePayIn(request);
        await action.Should().NotThrowAsync();
    }
}