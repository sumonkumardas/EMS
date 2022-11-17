using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeeLeaveListViewModel : BaseViewModel
  {
    public List<EmployeeLeaveViewModel> EmployeeLeaves = new List<EmployeeLeaveViewModel>();
  }
}
