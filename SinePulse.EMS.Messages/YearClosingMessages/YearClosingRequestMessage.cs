using MediatR;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.YearClosingMessages
{
  public class YearClosingRequestMessage : IRequest<ValidatedData<YearClosingResponseMessage>>
  {
    public long BranchMediumId { get; set; }
  }
}