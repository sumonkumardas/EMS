using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.TermTestMarkMessages
{
  public class GetTermTestMarksRequestMessage : IRequest<GetTermTestMarksResponseMessage>
  {
    public long ClassId { get; set; }
    public long SectionId { get; set; }
    public ExamTypeEnum ExamType { get; set; }
    public GroupEnum Group { get; set; }
    public long SubjectId { get; set; }
    public long TermId { get; set; }
    public long BranchMediumId { get; set; }
  }
}