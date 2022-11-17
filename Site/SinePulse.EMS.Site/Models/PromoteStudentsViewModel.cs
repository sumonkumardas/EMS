using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class PromoteStudentsViewModel : BaseViewModel
  {
    public long CurrentSectionId { get; set; }
    public long NextSessionId { get; set; }
    public long NextClassId { get; set; }

    public IList<Student> CurrentSectionStudents { get; set; }
    public IList<Section> NextSessionSections { get; set; }

    public class Student
    {
      public long StudentId { get; set; }
      public string StudentName { get; set; }
      public int StudentRoll { get; set; }
      public long StudentSectionId { get; set; }
      public long NextSectionId { get; set; }
    }

    public class Section
    {
      public long SectionId { get; set; }
      public string SectionText { get; set; }
    }
  }
}