using System.Collections.Specialized;

namespace SinePulse.EMS.Utility.SslPaymentGateway
{
  public class PaymentHelper : IPaymentHelper
  {
    public string GetPaymentGatewayUrl(string baseUrl, string amount, string transactionId, string consumerName,
      string consumerEmail,
      string consumerPhoneNo)
    {
      var postData = new NameValueCollection
      {
        {"total_amount", amount},
        {"tran_id", transactionId},
        {"success_url", baseUrl + SslPaymentData.SuccessUrl + transactionId},
        {"fail_url", baseUrl + SslPaymentData.FailUrl + transactionId},
        {"cancel_url", baseUrl + SslPaymentData.CancelUrl + transactionId},
        {"version", "3.00"},
        {"cus_name", consumerName},
        {"cus_email", consumerEmail},
        {"cus_phone", consumerPhoneNo},
        {"cus_add1", "Address Line On"},
        {"cus_add2", "Address Line Tw"},
        {"cus_city", "City Nam"},
        {"cus_state", "State Nam"},
        {"cus_postcode", "Post Cod"},
        {"cus_country", "Country"},
        {"cus_fax", "0171111111"},
        {"ship_name", "ABC XY"},
        {"ship_add1", "Address Line On"},
        {"ship_add2", "Address Line Tw"},
        {"ship_city", "City Nam"},
        {"ship_state", "State Nam"},
        {"ship_postcode", "Post Cod"},
        {"ship_country", "Country"},
        {"value_a", "ref00"},
        {"value_b", "ref00"},
        {"value_c", "ref00"},
        {"value_d", "ref00"}
      };
      var sslcz = new SSLCommerz(SslPaymentData.TestStoreId, SslPaymentData.TestStorePassword, SslPaymentData.PaymentTestMode);
      var response = sslcz.InitiateTransaction(postData);
      return response;
    }
  }
}