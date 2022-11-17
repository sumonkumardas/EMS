using MediatR;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class GetClassTestMarksRequestMessage : IRequest<GetClassTestMarksResponseMessage>
  {
    public long TermId { get; set; }
    public long StudentId { get; set; }
  }
}