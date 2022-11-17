namespace SinePulse.EMS.Messages.LoginMessages
{
  public class LoginResponseMessage
  {
    public bool IsSuccess { get; }

    public LoginResponseMessage(bool isSuccess)
    {
      IsSuccess = isSuccess;
    }
  }
}