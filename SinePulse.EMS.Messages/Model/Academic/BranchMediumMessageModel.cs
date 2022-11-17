using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Shared;
using System.Collections.Generic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class BranchMediumMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public WeekDays WeeklyHolidays { get; set; }
    #endregion

    #region  Navigation Properties
    public BranchMessageModel Branch { get; set; }
    public MediumMessageModel Medium { get; set; }
    public ShiftMessageModel Shift { get; set; }
    #endregion
  }
}