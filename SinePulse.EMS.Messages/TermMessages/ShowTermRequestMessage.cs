using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.TermMessages
{
  public class ShowTermRequestMessage : IRequest<ValidatedData<ShowTermResponseMessage>>
  {
    public long TermId { get; set; }
  }
}