using MediatR;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class AddOrChangeStudentImageRequestMessage : IRequest<AddOrChangeStudentImageResponseMessage>
  {
    public long StudentId { get; set; }
    public string ImageUrl { get; set; }
    public string CurrentUserName { get; set; }
  }
}