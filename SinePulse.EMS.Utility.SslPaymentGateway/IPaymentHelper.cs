namespace SinePulse.EMS.Utility.SslPaymentGateway
{
  public interface IPaymentHelper
  {
    string GetPaymentGatewayUrl(string baseUrl, string amount, string transactionId, string consumerName,
      string consumerEmail,
      string consumerPhoneNo);
  }
}