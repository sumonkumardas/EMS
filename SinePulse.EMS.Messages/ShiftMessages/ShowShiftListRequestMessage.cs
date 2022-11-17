using MediatR;

namespace SinePulse.EMS.Messages.ShiftMessages
{
  public class ShowShiftListRequestMessage : IRequest<ShowShiftListResponseMessage>
  {
    public long BranchId { get; set; }
  }
}