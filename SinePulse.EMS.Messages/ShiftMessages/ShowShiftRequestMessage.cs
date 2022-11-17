using MediatR;

namespace SinePulse.EMS.Messages.ShiftMessages
{
  public class ShowShiftRequestMessage : IRequest<ShowShiftResponseMessage>
  {
    public long ShiftId { get; set; }
  }
}