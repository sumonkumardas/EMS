using System;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Leaves
{
  public class EmployeeLeaveMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public LeaveStatusEnum LeaveStatus { get; set; }

    #endregion

    #region  Navigation Properties

    public LeaveTypeMessageModel LeaveType { get; set; }
    public EmployeeMessageModel Employee { get; set; }
    public EmployeeMessageModel ApproveBy { get; set; }

    #endregion
  }
}