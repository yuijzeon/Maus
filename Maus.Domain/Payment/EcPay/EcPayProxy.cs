using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public class EcPayProxy(HttpClient httpClient) : IEcPayProxy
{
    public async Task<EcPayQueryResponse> Query(string transactionNo, PaymentChannel paymentChannel)
    {
        var request = new EcPayQueryRequest(transactionNo, paymentChannel);
        var content = new FormUrlEncodedContent(request.ToStringDictionary());
        var responseMessage = await httpClient.PostAsync(paymentChannel.QueryUrl, content);
        responseMessage.EnsureSuccessStatusCode();
        var queryString = await responseMessage.Content.ReadAsStringAsync();
        var response = new EcPayQueryResponse(queryString);
        response.CheckSignature(paymentChannel.MerchantKey, paymentChannel.MerchantIv);
        return response;
    }
}