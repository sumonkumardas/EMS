using MediatR;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class ShowSessionListRequestMessage : IRequest<ShowSessionListResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public long MediumId { get; set; }
    public long BranchId { get; set; }
  }
}