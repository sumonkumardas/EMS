using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class EditTermTestViewModel: BaseViewModel
  {
    public long Id { get; set; }
    public decimal FullMarks { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public ExamTypeEnum ExamType { get; set; }
    public long ClassId { get; set; }
    public long SubjectId { get; set; }
    public long GroupId { get; set; }
    public long TermId { get; set; }
    public long TermBranchId { get; set; }
  }
}