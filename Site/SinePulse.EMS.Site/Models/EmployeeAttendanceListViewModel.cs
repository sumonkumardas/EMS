using System;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeeAttendanceListViewModel : BaseViewModel
  {
    public long InstituteId { get; set; }
    public long BranchId { get; set; }
    public long BranchMediumId { get; set; }
    public DateTime AttendanceStartDate { get; set; }
    public DateTime AttendanceEndDate { get; set; }
    public List<InstituteViewModel> InstituteList { get; set; }
    public List<BranchViewModel> BranchList { get; set; }
    public List<BranchMediumViewModel> BranchMediumList { get; set; }
    public List<EmployeeViewModel> EmployeeList { get; set; }
    public long EmployeeId { get; set; }
  }
}