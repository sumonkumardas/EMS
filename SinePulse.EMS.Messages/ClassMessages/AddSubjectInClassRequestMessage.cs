using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class AddSubjectInClassRequestMessage : IRequest<AddSubjectInClassResponseMessage>
  {
    public long ClassId { get; set; }
    public GroupEnum Group { get; set; }
    public int[] SubjectIds { get; set; }
    public bool IsOptional { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}