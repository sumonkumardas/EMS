using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.ClassTestMessages
{
  public class ShowClassTestResponseMessage
  {
    public ClassTest ClassTestData { get; }

    public ShowClassTestResponseMessage(ClassTest classTest)
    {
      ClassTestData = classTest;
    }

    public class ClassTest
    {
      public long TestId { get; set; }

      public string TestName { get; set; }

      public decimal FullMarks { get; set; }

      public DateTime StartTimestamp { get; set; }

      public DateTime EndTimestamp { get; set; }

      public ExamTypeEnum ExamType { get; set; }

      public Term Term { get; set; }

      public GroupEnum Group { get; set; }

      public Subject Subject { get; set; }

      public Section Section { get; set; }

      public long ExamConfigurationId { get; set; }

      public StatusEnum Status { get; set; }
    }

    public class Term
    {
      public long TermId { get; set; }

      public string TermName { get; set; }
    }

    public class Subject
    {
      public long SubjectId { get; set; }

      public string SubjectName { get; set; }
    }

    public class Section
    {
      public long SectionId { get; set; }
      public string SectionName { get; set; }
      public string ClassName { get; set; }
    }
  }
}