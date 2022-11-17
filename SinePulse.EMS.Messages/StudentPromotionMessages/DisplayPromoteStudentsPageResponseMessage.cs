using System.Collections.Generic;

namespace SinePulse.EMS.Messages.StudentPromotionMessages
{
  public class DisplayPromoteStudentsPageResponseMessage
  {
    public ICollection<Student> CurrentSectionStudents { get; set; }
    public ICollection<Section> NextSessionSections { get; set; }

    public class Student
    {
      public long StudentId { get; set; }
      public string StudentName { get; set; }
      public int StudentRoll { get; set; }
      public long StudentSectionId { get; set; }
    }

    public class Section
    {
      public long SectionId { get; set; }
      public string SectionName { get; set; }
      public int AvailableSeats { get; set; }
    }
  }
}