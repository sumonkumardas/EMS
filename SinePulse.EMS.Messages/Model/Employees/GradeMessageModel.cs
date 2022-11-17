using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Employees
{
  public class GradeMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string GradeTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public StatusEnum Status { get; set; }

    #endregion
  }
}