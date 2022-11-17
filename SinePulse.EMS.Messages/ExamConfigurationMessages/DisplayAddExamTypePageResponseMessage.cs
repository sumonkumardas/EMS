using System.Collections.Generic;

namespace SinePulse.EMS.Messages.ExamConfigurationMessages
{
  public class DisplayAddExamConfigurationPageResponseMessage
  {
    public ICollection<Class> Classes { get; }
    public ICollection<Subject> Subjects { get; }
    public ICollection<Group> Groups { get; }

    public DisplayAddExamConfigurationPageResponseMessage(ICollection<Class> classes, ICollection<Subject> subjects, ICollection<Group> groups)
    {
      Classes = classes;
      Subjects = subjects;
      Groups = groups;
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
    public class Group
    {
      public long GroupId { get; set; }
      public string GroupName { get; set; }
    }
  }
}