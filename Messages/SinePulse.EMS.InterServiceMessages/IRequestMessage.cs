namespace SinePulse.EMS.InterServiceMessages
{
  public interface IRequestMessage : IMessage
  {
    string RequestId { get; set; }
  }
}