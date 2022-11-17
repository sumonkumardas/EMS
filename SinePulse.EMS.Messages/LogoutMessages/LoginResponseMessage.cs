namespace SinePulse.EMS.Messages.LogoutMessages
{
  public class LogoutResponseMessage
  {
    public bool IsSuccess { get; }

    public LogoutResponseMessage(bool isSuccess)
    {
      IsSuccess = isSuccess;
    }
  }
}