using MediatR;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class ShowBranchMediumListRequestMessage : IRequest<ShowBranchMediumListResponseMessage>
  {
    public long BranchId { get; set; }
  }
}