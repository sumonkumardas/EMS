
using SinePulse.EMS.Messages.Model.Enums;
using System;

namespace SinePulse.EMS.Site.Models
{
  public class PublicHolidayViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string HolidayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusEnum Status { get; set; }
  }
}
