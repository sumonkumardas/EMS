using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinePulse.EMS.Site.Models
{
  public class AddEmployeeLeaveViewModel : BaseViewModel
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public LeaveStatusEnum LeaveStatus { get; set; }
    public long EmployeeId { get; set; }
    public long LeaveTypeId { get; set; }
    public List<LeaveTypeViewModel> LeaveTypes { get; set; }
  }
}