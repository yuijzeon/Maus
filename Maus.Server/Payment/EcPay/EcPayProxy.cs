using System.Collections.Specialized;
using System.Web;
using Maus.Server.Extensions;
using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;

namespace Maus.Server.Payment.EcPay;

public class EcPayProxy(HttpClient httpClient) : IEcPayProxy
{
    public async Task<EcPayQueryResponse> Query(string transactionNo, PaymentChannel paymentChannel)
    {
        var request = new EcPayQueryRequest(transactionNo, paymentChannel);
        var content = new FormUrlEncodedContent(request.ToStringDictionary());
        var responseMessage = await httpClient.PostAsync(paymentChannel.QueryUrl, content);
        responseMessage.EnsureSuccessStatusCode();
        var queryString = await responseMessage.Content.ReadAsStringAsync();
        var response = HttpUtility.ParseQueryString(queryString).ParseTo<EcPayQueryResponse>();
        response.CheckSignature(paymentChannel.MerchantKey, paymentChannel.MerchantIv);
        return response;
    }
}