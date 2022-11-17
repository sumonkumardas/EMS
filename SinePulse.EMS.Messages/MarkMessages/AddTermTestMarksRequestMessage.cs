using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class AddTermTestMarksRequestMessage : IRequest<AddTermTestMarksResponseMessage>
  {
    public decimal[] ObtainedMarks { get; set; }
    public decimal[] GraceMarks { get; set; }
    public long TermId { get; set; }
    public long[] StudentSectionIds { get; set; }
    public string CurrentUserName { get; set; }
    public decimal FullMarks { get; set; }
    public long TermTestId { get; set; }
    public long ClassId { get; set; }
    public long SectionId { get; set; }
    public ExamTypeEnum ExamType { get; set; }
    public GroupEnum Group { get; set; }
    public long SubjectId { get; set; }
    public string[] RemarksArray { get; set; }
    
  }
}