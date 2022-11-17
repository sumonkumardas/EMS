namespace SinePulse.EMS.Messages.MarkMessages
{
  public class AddMarkResponseMessage
  {
    public long MarkId { get; }

    public AddMarkResponseMessage(long markId)
    {
      MarkId = markId;
    }
  }
}