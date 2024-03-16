using Maus.Domain.Extensions;
using Maus.Domain.Payment.Core;
using Maus.Domain.Payment.EcPay.Interfaces;
using Maus.Domain.Payment.EcPay.Models;

namespace Maus.Domain.Payment.EcPay;

public class EcPayProxy(HttpClient httpClient) : IEcPayProxy
{
    public async Task<EcPayQueryResponse> Query(string transactionNo, PaymentChannel channel)
    {
        var request = new EcPayQueryRequest(transactionNo, channel);
        var content = new FormUrlEncodedContent(request.ToStringDictionary());
        var responseMessage = await httpClient.PostAsync(channel.QueryUrl, content);
        responseMessage.EnsureSuccessStatusCode();
        var queryString = await responseMessage.Content.ReadAsStringAsync();
        var response = new EcPayQueryResponse(queryString, channel);
        return response;
    }
}