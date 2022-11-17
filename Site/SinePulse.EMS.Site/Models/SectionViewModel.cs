using System;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class SectionViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public GroupEnum Group { get; set; }
    public string SectionName { get; set; }
    public int NumberOfStudents { get; set; }
    public int TotalClasses { get; set; }
    public int DurationOfClass { get; set; }
    public StatusEnum Status { get; set; }
    public ClassViewModel Class { get; set; }
    public SessionViewModel Session { get; set; }
    public BranchMediumViewModel BranchMedium { get; set; }
    public IEnumerable<TestViewModel> ClassTests { get; set; } = new List<TestViewModel>();
    public IEnumerable<ExamRoutineViewModel> Routines { get; set; } = new List<ExamRoutineViewModel>();

  }
}