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
        var httpClient = Substitute.For<HttpClient>();
        _paymentProxy = new EcPayProxy(httpClient);
    }

    [TestMethod]
    public async Task ec_pay_create_order()
    {
        var request = new PaymentRequest();
        var action = async () => await _paymentProxy.CreatePayIn(request);
        await action.Should().NotThrowAsync();
    }
}