using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;

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
        var request = new PaymentRequest();
        var action = async () => await _ecPayApplyService.CreatePayIn(request);
        await action.Should().NotThrowAsync();
    }
}