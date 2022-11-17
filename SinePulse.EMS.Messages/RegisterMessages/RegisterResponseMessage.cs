namespace SinePulse.EMS.Messages.RegisterMessages
{
  public class RegisterResponseMessage
  {
    public bool IsSuccess { get; }

    public RegisterResponseMessage(bool isSuccess)
    {
      IsSuccess = isSuccess;
    }
  }
}