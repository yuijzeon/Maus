﻿using ECPay.Payment.Integration;
using HttpMethod = ECPay.Payment.Integration.HttpMethod;

namespace Maus.Domain.Payment.EcPay;

public class EcPayProxy(HttpClient httpClient) : IPaymentProxy
{
    public async Task<object> CreatePayIn(PaymentRequest request)
    {
        using var oPayment = new AllInOne();

        /* 服務參數 */
        oPayment.ServiceMethod = HttpMethod.HttpPOST; //介接服務時，呼叫 API 的方法
        oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5"; //要呼叫介接服務的網址
        oPayment.HashKey = "5294y06JbISpM5x9"; //ECPay提供的Hash Key
        oPayment.HashIV = "v77hoKGq4kWxNNIS"; //ECPay提供的Hash IV
        oPayment.MerchantID = "2000132"; //ECPay提供的特店編號

        /* 基本參數 */
        oPayment.Send.ReturnURL = "http://example.com"; //付款完成通知回傳的網址
        oPayment.Send.ClientBackURL = "http://www.ecpay.com.tw/"; //瀏覽器端返回的廠商網址
        oPayment.Send.OrderResultURL = "http://localhost:52413/CheckOutFeedback.aspx"; //瀏覽器端回傳付款結果網址
        oPayment.Send.MerchantTradeNo = "ECPay" + new Random().Next(0, 99999); //廠商的交易編號
        oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //廠商的交易時間
        oPayment.Send.TotalAmount = decimal.Parse("3280"); //交易總金額
        oPayment.Send.TradeDesc = "交易描述"; //交易描述
        oPayment.Send.ChoosePayment = PaymentMethod.ALL; //使用的付款方式
        oPayment.Send.Remark = ""; //備註欄位
        oPayment.Send.ChooseSubPayment = PaymentMethodItem.None; //使用的付款子項目
        oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.Yes; //是否需要額外的付款資訊
        oPayment.Send.DeviceSource = DeviceType.PC; //來源裝置
        oPayment.Send.IgnorePayment = ""; //不顯示的付款方式
        oPayment.Send.PlatformID = ""; //特約合作平台商代號
        oPayment.Send.CustomField1 = "";
        oPayment.Send.CustomField2 = "";
        oPayment.Send.CustomField3 = "";
        oPayment.Send.CustomField4 = "";
        oPayment.Send.EncryptType = 1;

        //訂單的商品資料
        oPayment.Send.Items.Add(new Item
        {
            Name = "蘋果", //商品名稱
            Price = decimal.Parse("3280"), //商品單價
            Currency = "新台幣", //幣別單位
            Quantity = int.Parse("1"), //購買數量
            URL = "http://google.com" //商品的說明網址
        });

        /* 產生訂單 */
        var allInOneResult = oPayment.CheckOut();

        return Task.FromResult(allInOneResult);
    }
}