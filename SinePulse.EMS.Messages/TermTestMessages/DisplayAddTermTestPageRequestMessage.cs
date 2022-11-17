using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class DisplayAddTermTestPageRequestMessage : IRequest<ValidatedData<DisplayAddTermTestPageResponseMessage>>
  {
    public long? ClassId { get; set; }
    public long? GroupId { get; set; }
    public long? SubjectId { get; set; }
    public long TermId { get; set; }
    public long? BranchId { get; set; }
  }
}