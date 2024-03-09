using Maus.Server.Extensions;
using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;

namespace Maus.Server.Payment.EcPay;

public class EcPayProxy(HttpClient httpClient) : IEcPayProxy
{
    public async Task<string> AioCheckOut(PaymentChannel paymentChannel, OrderDetail orderDetail)
    {
        var request = new EcPayAioDepositRequest(paymentChannel, orderDetail);
        var content = new FormUrlEncodedContent(request.ToStringDictionary());
        var response = await httpClient.PostAsync(paymentChannel.SubmitUrl, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}