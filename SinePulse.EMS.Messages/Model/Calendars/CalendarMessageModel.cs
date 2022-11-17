using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using System;


namespace SinePulse.EMS.Messages.Model.Calendars
{
  public class CalendarMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public CalendarTypeEnum CalendarType { get; set; }
    public StatusEnum Status { get; set; }
    #endregion
  }
}
