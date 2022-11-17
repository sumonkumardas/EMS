namespace SinePulse.EMS.Messages.StudentMessages
{
  public class AddStudentResponseMessage
  {
    public long StudentId { get; }

    public AddStudentResponseMessage(long studentId)
    {
      StudentId = studentId;
    }
  }
}