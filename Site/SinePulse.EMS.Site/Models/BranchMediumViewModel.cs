using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class BranchMediumViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string BranchName {
      get
      {
        
        return (Branch == null?"":Branch.BranchName) + "->" +(Medium == null?"": Medium.MediumName);
      }
    }
    public long MediumId { get; set; }
    public WeekDays WeeklyHolidays { get; set; }
    public BranchViewModel Branch { get; set; }
    public MediumViewModel Medium { get; set; }
    public ShiftViewModel Shift { get; set; }
  }
}