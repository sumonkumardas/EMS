using MediatR;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class GetTermTestAddMarkObjectsRequestMessage : IRequest<GetTermTestAddMarkObjectsResponseMessage>
  {
    public long ClassId { get; set; }
    public long Group { get; set; }
    public long SubjectId { get; set; }
    public long ExamType { get; set; }
    public long SectionId { get; set; }
    public long TermId { get; set; }
    public long BranchMediumId { get; set; }
  }
}