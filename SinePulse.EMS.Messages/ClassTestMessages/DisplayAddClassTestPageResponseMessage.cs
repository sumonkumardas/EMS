using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using GroupEnum = SinePulse.EMS.Messages.Model.Enums.GroupEnum;

namespace SinePulse.EMS.Messages.ClassTestMessage
{
  public class DisplayAddClassTestPageResponseMessage
  {
    public ICollection<Term> Terms { get; set; }

    public string ClassName { get; set; }
    public long SectionId { get; set; }

    public class Term
    {
      public long TermId { get; set; }
      public string TermName { get; set; }
      public ICollection<Group> Groups { get; set; }
    }

    public class Group
    {
      public string GroupEnum { get; set; }
      public ICollection<Subject> Subjects { get; set; }
    }

    public class Subject
    {
      public string SubjectName { get; set; }
      public long ExamConfigurationId { get; set; }
    }
  }
}