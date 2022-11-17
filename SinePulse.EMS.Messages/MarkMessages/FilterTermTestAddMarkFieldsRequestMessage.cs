using MediatR;
using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class FilterTermTestAddMarkFieldsRequestMessage : IRequest<FilterTermTestAddMarkFieldsResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public long TermId { get; set; }
    public long ClassId { get; set; }
    public long Group { get; set; }
    public long SubjectId { get; set; }
  }
}