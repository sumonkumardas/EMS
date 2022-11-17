using MediatR;

namespace SinePulse.EMS.Messages.BranchMessages
{
  public class ShowBranchListRequestMessage : IRequest<ShowBranchListResponseMessage>
  {
    public long InstituteId;
  }
}