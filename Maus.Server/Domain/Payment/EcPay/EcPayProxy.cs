using Maus.Server.Domain.Payment.EcPay.Models;
using Maus.Server.Extensions;

namespace Maus.Server.Domain.Payment.EcPay;

public class EcPayProxy(HttpClient httpClient) : IEcPayProxy
{
    public async Task<string> AioCheckOut(PaymentChannel paymentChannel, OrderDetail orderDetail)
    {
        var request = new EcPayAllPaymentRequest(paymentChannel, orderDetail);
        var content = new FormUrlEncodedContent(request.ToStringDictionary());
        var response = await httpClient.PostAsync(paymentChannel.SubmitUrl, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}