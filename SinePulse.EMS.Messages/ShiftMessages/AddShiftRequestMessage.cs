using System;
using MediatR;

namespace SinePulse.EMS.Messages.ShiftMessages
{
  public class AddShiftRequestMessage : IRequest<AddShiftResponseMessage>
  {
    public string ShiftName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string CurrentUserName { get; set; }
    public long BranchId { get; set; }
  }
}