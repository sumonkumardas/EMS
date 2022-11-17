
namespace SinePulse.EMS.Messages.StudentContactPersonMessages
{
  public class AddStudentContactPersonImageRequestMessage : MediatR.IRequest<AddStudentContactPersonImageResponseMessage>
  {
    public long ContactPersonId { get; set; }
    public string ImageUrl { get; set; }
    public string CurrentUserName { get; set; }
  }
}