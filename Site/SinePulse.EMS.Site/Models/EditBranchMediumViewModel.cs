using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class EditBranchMediumViewModel : BaseViewModel
  {
    public long BranchId { get; set; }
    public long BranchMediumId { get; set; }
    public long MediumId { get; set; }
    public long ShiftId { get; set; }
    public string BranchName { get; set; }
    public IEnumerable<WeekDays> WeeklyHolidaysList { get; set; }
    public int SessionBufferPeriod { get; set; }
    public IEnumerable<ShiftMessageModel> Shifts { get; set; }
    public IEnumerable<MediumMessageModel> Mediums { get; set; }
  }
}