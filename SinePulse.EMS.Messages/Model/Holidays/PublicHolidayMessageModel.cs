using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using System;


namespace SinePulse.EMS.Messages.Model.Holidays
{
  public class PublicHolidayMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string HolidayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusEnum Status { get; set; }
    #endregion
  }
}
