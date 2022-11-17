using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ResultGradeMessages
{
  public class ShowResultGradeListRequestMessage
    : IRequest<ValidatedData<ShowResultGradeListResponseMessage>>
  {
    public long BranchMediumId { get; set; }
  }
}