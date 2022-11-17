using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Employees
{
    public class EducationalQualificationMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public  string InstituteName { get; set; }
    public  EducationDegreeEnum DegreeName { get; set; }
    public  string MajorSubject { get; set; }
    public  string PassingYear { get; set; }
    #endregion

    #region  Navigation Properties
    public  EmployeeMessageModel Employee { get; set; }
    #endregion
  }
}
