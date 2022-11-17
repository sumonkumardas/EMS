using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class ShowSeatPlanListRequestMessage : IRequest<ValidatedData<ShowSeatPlanListResponseMessage>>
  {
    public long TermId { get; set; }
  }
}