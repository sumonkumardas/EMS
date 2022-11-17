using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class AddEmployeePersonalInfoViewModel : BaseViewModel
  {
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string SpouseName { get; set; }
    public GenderEnum Gender { get; set; }
    public ReligionEnum Religion { get; set; }
    public BloodGroupEnum BloodGroup { get; set; }
    public long EmployeeId { get; set; }
  }
}
