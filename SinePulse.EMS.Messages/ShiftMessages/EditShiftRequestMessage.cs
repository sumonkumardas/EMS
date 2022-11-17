using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.ShiftMessages
{
  public class EditShiftRequestMessage : IRequest<EditShiftResponseMessage>
  {
    public long ShiftId { get; set; }
    public string ShiftName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public StatusEnum Status { get; set; }
    public long BranchId { get; set; }
    public string CurrentUserName { get; set; }
  }
}