using MediatR;

namespace SinePulse.EMS.Messages.JobTypeMessages
{
  public class ShowJobTypeRequestMessage : IRequest<ShowJobTypeResponseMessage>
  {
    public long JobTypeId { get; set; } 
  }
}