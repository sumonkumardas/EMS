namespace SinePulse.EMS.Utility.SslPaymentGateway
{
  public class SslPaymentData
  {
    public const string SuccessUrl = "/OnlinePayment/TransactionSuccess?transactionId=";
    public const string FailUrl = "/OnlinePayment/TransactionFailure?transactionId=";
    public const string CancelUrl = "/OnlinePayment/TransactionCanceled?transactionId=";
    public const string TestStoreId = "testbox";
    public const string TestStorePassword = "qwerty";
    public const string LiveStoreId = "";
    public const string LiveStorePassword = "";
    public const bool PaymentTestMode = true;
    public const bool PaymentLiveMode = false;
  }
}