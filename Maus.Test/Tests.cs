using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;

namespace Maus.Test;

[TestClass]
public class Tests
{
    [TestInitialize]
    public void Initialize()
    {
    }

    [TestMethod]
    public async Task ec_pay_create_order()
    {
        "EcPay".ToEnumOrDefault<ProviderCode>().Should().Be(ProviderCode.EcPay);
        "abc".ParseOrDefault<decimal>().Should().Be(0);
        "".ParseOrDefault<decimal>().Should().Be(0);
    }
}