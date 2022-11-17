using MediatR;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Messages.LeaveTypeMessages
{
  public class AddLeaveTypeRequestMessage : IRequest<AddLeaveTypeResponseMessage>
  {
    public string LeaveName { get; set; }
    public int LeavesPerYear { get; set; }
    public bool CanEmployeesApplyBeyondTheCurrentLeaveBalance { get; set; }
    public bool WillLeaveCarriedForward { get; set; }
    public int? PercentageOfLeaveCarriedForward { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}