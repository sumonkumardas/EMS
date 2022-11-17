using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class LeaveTypeViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string LeaveName { get; set; }
    public int LeavesPerYear { get; set; }
    public bool CanEmployeesApplyBeyondTheCurrentLeaveBalance { get; set; }
    public bool WillLeaveCarriedForward { get; set; }
    public int? PercentageOfLeaveCarriedForward { get; set; }
    public StatusEnum Status { get; set; }
  }
}