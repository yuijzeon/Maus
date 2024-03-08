using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;

namespace Maus.Test;

[TestClass]
public class EcPayProxyTests
{
    private IPaymentProxy _paymentProxy = null!;

    [TestInitialize]
    public void Initialize()
    {
        _paymentProxy = new EcPayProxy();
    }

    [TestMethod]
    public async Task ec_pay_create_order()
    {
        var request = new PaymentRequest();
        var action = async () => await _paymentProxy.CreatePayIn(request);
        await action.Should().NotThrowAsync();
    }
}