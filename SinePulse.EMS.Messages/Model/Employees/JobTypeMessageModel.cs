using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Employees
{
  public class JobTypeMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string JobTitle { get; set; }
    public bool HasOverTime { get; set; }
    public StatusEnum Status { get; set; }

    #endregion
  }
}