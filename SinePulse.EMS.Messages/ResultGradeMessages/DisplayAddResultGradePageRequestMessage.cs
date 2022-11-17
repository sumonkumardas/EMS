using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ResultGradeMessages
{
  public class DisplayAddResultGradePageRequestMessage
    : IRequest<ValidatedData<DisplayAddResultGradePageResponseMessage>>
  {
    public long BranchMediumId { get; set; }
  }
}