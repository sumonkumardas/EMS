using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.WorkingShiftMessages
{
  public class AddWorkingShiftRequestMessage : IRequest<AddWorkingShiftResponseMessage>
  {
    public string ShiftName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public StatusEnum Status { get; set; }
    
    public string CurrentUserName { get; set; }
  }
}