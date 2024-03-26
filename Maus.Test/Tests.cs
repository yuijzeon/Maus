using Maus.Domain.Payment.Deposit;
using Maus.Domain.Payment.EcPay;
using Maus.Domain.Payment.EcPay.Interfaces;

namespace Maus.Test;

[TestClass]
public class Tests
{
    [TestInitialize]
    public void Initialize()
    {
    }

    [DataTestMethod]
    [DataRow("ABBY", "ab12-3456-7890", "ABBYxAB1234567890")]
    public void ec_pay_transaction_no(string merchantCode, string merchantTransactionNo, string excepted)
    {
        var ecPayService = new EcPayService(Substitute.For<IDepositRepository>(), Substitute.For<IEcPayProxy>());
        var transactionNo = ecPayService.GetTransactionNo(merchantCode, merchantTransactionNo);
        transactionNo.Should().Be(excepted);
    }
}