using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class TermViewModel : BaseViewModel
  {
    public long TermId { get; set; }
    public string TermName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusEnum Status { get; set; }
    public BranchMedium BranchMediumData { get; set; }
    public Session SessionData { get; set; }
    public AddTermTestMarksViewModel AddTermTestMarksViewModel { get; set; }= new AddTermTestMarksViewModel();
    
    public class BranchMedium
    {
      public long Id { get; set; }
      public string InstituteName { get; set; }
      public string BranchName { get; set; }
      public string MediumName { get; set; }
    }
    public class Session
    {
      public long SessionId { get; set; }
      public string SessionText { get; set; }
    }
    public virtual ICollection<ExamConfiguration> ExamConfigurations { get; set; } = new List<ExamConfiguration>();
  }
}