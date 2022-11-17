using MediatR;

namespace SinePulse.EMS.Messages.ResultSheetMessages
{
  public class GetTermResultSheetRequestMessage : IRequest<GetTermResultSheetResponseMessage>
  {
    public long ExamTermId { get; set; }
    public long StudentId { get; set; }
    public long SessionId { get; set; }
  }
}