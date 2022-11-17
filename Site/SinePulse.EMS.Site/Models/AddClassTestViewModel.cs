using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddClassTestViewModel : BaseViewModel
  {
    public string TestName { get; set; }
    public decimal FullMarks { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public ExamTypeEnum ExamType { get; set; }

    public ICollection<Term> Terms { get; set; }
    public long SectionId { get; set; }
    public long TermId { get; set; }
    public string ClassText { get; set; }
    public string GroupName { get; set; }
    public long ExamConfigurationId { get; set; }

    public class Term
    {
      public long TermId { get; set; }
      public string TermName { get; set; }
      public ICollection<Group> Groups { get; set; }
    }

    public class Group
    {
      public string GroupName { get; set; }
      public ICollection<Subject> Subjects { get; set; }
    }

    public class Subject
    {
      public string SubjectName { get; set; }
      public long ExamConfigurationId { get; set; }
    }
  }
}