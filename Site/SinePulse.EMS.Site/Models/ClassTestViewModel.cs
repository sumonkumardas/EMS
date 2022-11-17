using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class ClassTestViewModel : BaseViewModel
  {
    public long ClassTestId { get; set; }

    public string ClassTestName { get; set; }

    public decimal FullMarks { get; set; }

    public DateTime StartTimestamp { get; set; }

    public DateTime EndTimestamp { get; set; }

    public ExamTypeEnum ExamType { get; set; }

    public Term TermData { get; set; }

    public string Group { get; set; }

    public Subject SubjectData { get; set; }

    public Section SectionData { get; set; }

    public long ExamConfigurationId { get; set; }

    public StatusEnum Status { get; set; }

    public class Term
    {
      public long TermId { get; set; }

      public string TermText { get; set; }
    }

    public class Subject
    {
      public long SubjectId { get; set; }

      public string SubjectText { get; set; }
    }

    public class Section
    {
      public long SectionId { get; set; }
      public string SectionText { get; set; }
    }
  }
}