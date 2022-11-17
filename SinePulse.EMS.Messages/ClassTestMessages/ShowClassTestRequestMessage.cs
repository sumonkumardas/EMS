using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ClassTestMessages
{
  public class ShowClassTestRequestMessage : IRequest<ValidatedData<ShowClassTestResponseMessage>>
  {
    public long ClassTestId { get; set; }
  }
}