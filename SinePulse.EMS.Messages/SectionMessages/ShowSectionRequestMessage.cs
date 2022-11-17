using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class ShowSectionRequestMessage : IRequest<ShowSectionResponseMessage>
  {
    public long SectionId { get; set; }
  }
}