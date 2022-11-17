using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeeLeaveViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public LeaveStatusEnum LeaveStatus { get; set; }
    public LeaveTypeViewModel LeaveType { get; set; }
    public EmployeeViewModel Employee { get; set; }
  }
}