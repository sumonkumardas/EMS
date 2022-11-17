using MediatR;
using SinePulse.EMS.Messages.ClassTestMessage;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ClassTestMessages
{
  public class DisplayAddClassTestPageRequestMessage
    : IRequest<ValidatedData<DisplayAddClassTestPageResponseMessage>>
  {
    public long SectionId { get; set; }
  }
}