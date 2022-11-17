using System.Collections.Generic;
using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeeListViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }

    public List<ShowEmployeeListResponseMessage.Employee> EmployeeList { get; set; } =
      new List<ShowEmployeeListResponseMessage.Employee>();
  }
}