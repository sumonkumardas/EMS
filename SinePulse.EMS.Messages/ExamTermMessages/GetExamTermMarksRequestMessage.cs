using MediatR;

namespace SinePulse.EMS.Messages.ExamTermMessages
{
  public class GetExamTermMarksRequestMessage : IRequest<GetExamTermMarksResponseMessage>
  {
    public long ExamTermId { get; set; }
    public long StudentId { get; set; }
    public long SectionId { get; set; }
    public long ClassId { get; set; }
  }
}