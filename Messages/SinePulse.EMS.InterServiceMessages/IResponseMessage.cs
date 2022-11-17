namespace SinePulse.EMS.InterServiceMessages
{
  public interface IResponseMessage : IMessage
  {
    string RequestId { get; set; }
    string ResponseId { get; set; }
    Error Error { get; set; }
  }
}