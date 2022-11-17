using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.ExamConfigurationMessages
{
  public class ShowExamConfigurationResponseMessage
  {
    public ExamConfiguration ExamConfigurationData { get; }

    public ShowExamConfigurationResponseMessage(ExamConfiguration examConfiguration)
    {
      ExamConfigurationData = examConfiguration;
    }

    public class ExamConfiguration
    {
      public long ExamConfigurationId { get; set; }
      public int SubjectiveFullMark { get; set; }
      public int SubjectivePassMark { get; set; }
      public int ObjectiveFullMark { get; set; }
      public int ObjectivePassMark { get; set; }
      public int PracticalFullMark { get; set; }
      public int PracticalPassMark { get; set; }
      public int ClassTestPercentage { get; set; }

      public StatusEnum Status { get; set; }
      public Term Term { get; set; }
      public ClassSubject ClassSubject { get; set; }
      public Class Class { get; set; }
      public Subject Subject { get; set; }
    }

    public class Term
    {
      public long TermId { get; set; }
      public string TermName { get; set; }
    }

    public class ClassSubject
    {
      public long ClassSubjectId { get; set; }
      public GroupEnum Group { get; set; }
      public string SubjectName { get; set; }
      public string ClassName { get; set; }
    }
    public class Class
    {
      public long ClassId { get; set; }
      public string ClassName { get; set; }
    }
    public class Subject
    {
      public long SubjectId { get; set; }
      public string SubjectName { get; set; }
    }
  }
}