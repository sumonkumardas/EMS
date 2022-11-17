using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class AddEmployeeEducationalQualificationViewModel : BaseViewModel
  {
    public string InstituteName { get; set; }
    public EducationDegreeEnum DegreeName { get; set; }
    public string MajorSubject { get; set; }
    public string PassingYear { get; set; }
    public long EmployeeId { get; set; }
  }
}
