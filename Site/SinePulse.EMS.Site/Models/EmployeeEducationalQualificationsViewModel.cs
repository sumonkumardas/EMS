using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeeEducationalQualificationsViewModel : BaseViewModel
  {
    public IEnumerable<EducationalQualificationMessageModel> EducationalQualifications { get; set; }
    public long EmployeeId { get; set; }
  }
}