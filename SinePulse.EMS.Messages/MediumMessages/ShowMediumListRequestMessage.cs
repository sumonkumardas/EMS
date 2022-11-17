using MediatR;

namespace SinePulse.EMS.Messages.MediumMessages
{
  public class ShowMediumListRequestMessage : IRequest<ShowMediumListResponseMessage>
  {
    public long BranchId { get; set; }
  }
}