using Maus.Domain.Payment;
using Maus.Domain.Payment.EcPay;

namespace Maus.Test;

[TestClass]
public class EcPayServiceTests
{
    private IPaymentProvider _paymentProvider = null!;

    [TestInitialize]
    public void Initialize()
    {
        _paymentProvider = new EcPayService();
    }

    [TestMethod]
    public async Task ec_pay_create_order()
    {
        var request = new PaymentRequest();

        await _paymentProvider.PayAsync(request);
    }
}