using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.JobTypeMessages
{
  public class AddJobTypeRequestMessage : IRequest<AddJobTypeResponseMessage>
  {
    public string Title { get; set; }
    public bool HasOverTime { get; set; }
    public StatusEnum Status { get; set; }
    
    public string CurrentUserName { get; set; }
  }
}