using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class ShowServiceChargeListRequestMessage : IRequest<ValidatedData<ShowServiceChargeListResponseMessage>>
  {
    public long BranchMediumId { get; set; }
  }
}
