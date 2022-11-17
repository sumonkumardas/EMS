using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.TermMessages
{
  public class DisplayAddTermPageRequestMessage : IRequest<ValidatedData<DisplayAddTermPageResponseMessage>>
  {
    public long BranchMediumId { get; set; }
  }
}