using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.Messages.TermMessages
{
  public class ShowTermResponseMessage
  {
    public Term TermData { get; }

    public ShowTermResponseMessage(Term termData)
    {
      TermData = termData;
    }

    public class Term
    {
      public long Id { get; set; }

      public string TermName { get; set; }

      public DateTime StartDate { get; set; }

      public DateTime EndDate { get; set; }

      public StatusEnum Status { get; set; }

      public SingleLineSessionResult Session { set; get; }

      public BranchMedium BranchMedium { set; get; }
      public virtual ICollection<ExamConfiguration> ExamTypes { get; set; } = new List<ExamConfiguration>();
    }

    public class BranchMedium
    {
      public long Id { get; set; }
      public string InstituteName { get; set; }
      public string BranchName { get; set; }
      public long BranchId { get; set; }
      public string MediumName { get; set; }
    }
  }
}