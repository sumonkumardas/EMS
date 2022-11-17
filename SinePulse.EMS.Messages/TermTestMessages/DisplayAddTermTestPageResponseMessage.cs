using System.Collections.Generic;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class DisplayAddTermTestPageResponseMessage
  {
    public ICollection<Class> Classes { get; }
    public ICollection<Subject> Subjects { get; }
    public ICollection<Group> Groups { get; }
    public ICollection<Room> Rooms { get; }

    public DisplayAddTermTestPageResponseMessage(ICollection<Class> classes, ICollection<Subject> subjects, ICollection<Group> groups, ICollection<Room> rooms)
    {
      Classes = classes;
      Subjects = subjects;
      Groups = groups;
      Rooms = rooms;
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
    public class Room
    {
      public long RoomId { get; set; }
      public string RoomName { get; set; }
    }


  }
}