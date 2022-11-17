using MediatR;

namespace SinePulse.EMS.Messages.StudentSectionMessages
{
  public class AddStudentSectionRequestMessage : IRequest<AddStudentSectionResponseMessage>
  {
    public long StudentId { get; set; }
    public long SectionId { get; set; }
    public int RollNo { get; set; }
    public string CurrentUserName { get; set; }
  }
}