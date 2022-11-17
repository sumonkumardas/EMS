using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class ShowMarkResponseMessage
  {
    public Mark MarkData { get; }

    public ShowMarkResponseMessage(Mark termData)
    {
      MarkData = termData;
    }

    public class Mark
    {
      public long Id { get; set; }
      public decimal ObtainedMark { get; set; }
      public decimal GraceMark { get; set; }
      public string Comment { get; set; }
      public StatusEnum Status { get; set; }
      public Student Student { set; get; }
      public Test Test { set; get; }
    }

    public class Test
    {
      public long TestId { get; set; }
      public string TestName { get; set; }
    }
    public class Student
    {
      public long StudentSectionId { get; set; }
      public int RollNo { get; set; }
      public string StudentName { get; set; }
      public string ClassName { get; set; }
      public string ShiftName { get; set; }
      public string SectionName { get; set; }
    }
  }
}