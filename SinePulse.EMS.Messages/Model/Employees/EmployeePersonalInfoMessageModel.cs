using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;


namespace SinePulse.EMS.Messages.Model.Employees
{
  public class EmployeePersonalInfoMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string SpouseName { get; set; }
    public GenderEnum Gender { get; set; }
    public ReligionEnum Religion { get; set; }
    public BloodGroupEnum BloodGroup { get; set; }
    #endregion 

    #region  Navigation Properties
    public EmployeeMessageModel Employee { get; set; }
    #endregion
  }
}
