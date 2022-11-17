using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.JobTypeMessages
{
  public class EditJobTypeRequestMessage : IRequest<EditJobTypeResponseMessage>
  {
    public long JobTypeId { get; set; }
    public string JobTitle { get; set; }
    public bool HasOverTime { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}