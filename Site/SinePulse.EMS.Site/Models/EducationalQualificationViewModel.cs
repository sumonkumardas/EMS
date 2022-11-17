using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class EducationalQualificationViewModel : BaseViewModel
  {
    public long EducationalQualificationId { get; set; }
    public string InstituteName { get; set; }
    public EducationDegreeEnum DegreeName { get; set; }
    public string MajorSubject { get; set; }
    public string PassingYear { get; set; }
    public long EmployeeId { get; set; }
  }
}