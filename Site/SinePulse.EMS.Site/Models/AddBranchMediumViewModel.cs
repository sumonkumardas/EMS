using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class AddBranchMediumViewModel: BaseViewModel
  {
    public long BranchId { get; set; }
    public string BranchName { get; set; }
    public string MediumId { get; set; }
    public string ShiftId { get; set; }
    public IEnumerable<WeekDays> WeeklyHolidaysList { get; set; }
    public IEnumerable<ShiftMessageModel> Shifts { get; set; }
    public IEnumerable<MediumMessageModel> Mediums { get; set; }
    public int SessionBufferPeriod { get; set; }
  }
}