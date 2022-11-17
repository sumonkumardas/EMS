using MediatR;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.Messages.MediumMessages
{
  public class ShowMediumRequestMessage : IRequest<ShowMediumResponseMessage>
  {
    public long MediumId { get; set; }
    public Medium Medium { get; set; }
  }
}