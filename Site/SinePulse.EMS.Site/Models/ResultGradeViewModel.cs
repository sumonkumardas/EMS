using AutoMapper;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class ResultGradeViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string GradeLetter { get; set; }
    public decimal GradePoint { get; set; }
    public int StartMark { get; set; }
    public int EndMark { get; set; }
    public StatusEnum Status { get; set; }
    public BranchMediumMessageModel BranchMedium { get; set; }
  }
}