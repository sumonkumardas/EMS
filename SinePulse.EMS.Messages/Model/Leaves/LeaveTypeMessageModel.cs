using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Leaves
{
  public class LeaveTypeMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string LeaveName { get; set; }
    public int LeavesPerYear { get; set; }
    public bool CanEmployeesApplyBeyondTheCurrentLeaveBalance { get; set; }
    public bool WillLeaveCarriedForward { get; set; }
    public int PercentageOfLeaveCarriedForward { get; set; }
    public StatusEnum Status { get; set; }

    #endregion
  }
}