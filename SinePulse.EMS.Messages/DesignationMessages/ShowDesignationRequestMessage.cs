using MediatR;

namespace SinePulse.EMS.Messages.DesignationMessages
{
  public class ShowDesignationRequestMessage : IRequest<ShowDesignationResponseMessage>
  {
    public long DesignationId { get; set; }
  }
}