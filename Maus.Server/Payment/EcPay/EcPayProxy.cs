using System.Collections.Specialized;
using System.Web;
using Maus.Server.Extensions;
using Maus.Server.Payment.EcPay.Interfaces;
using Maus.Server.Payment.EcPay.Models;

namespace Maus.Server.Payment.EcPay;

public class EcPayProxy(HttpClient httpClient) : IEcPayProxy
{
    /*public async Task<string> AioCheckOut(PaymentChannel paymentChannel, Transaction transaction)
    {
        var request = new EcPayDepositRequest(paymentChannel, transaction);
        var content = new FormUrlEncodedContent(request.ToStringDictionary());
        var response = await httpClient.PostAsync(paymentChannel.SubmitUrl, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }*/

    public async Task<EcPayQueryResponse> Query(string transactionNo, PaymentChannel paymentChannel)
    {
        var request = new EcPayQueryRequest(transactionNo, paymentChannel);
        var content = new FormUrlEncodedContent(request.ToStringDictionary());
        var response = await httpClient.PostAsync(paymentChannel.QueryUrl, content);
        response.EnsureSuccessStatusCode();
        var queryString = await response.Content.ReadAsStringAsync();
        return HttpUtility.ParseQueryString(queryString).Parse<EcPayQueryResponse>();
    }
}