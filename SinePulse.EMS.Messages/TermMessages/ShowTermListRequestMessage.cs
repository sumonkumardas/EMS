using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.TermMessages
{
  public class ShowTermListRequestMessage : IRequest<ValidatedData<ShowTermListResponseMessage>>
  {
    public long BranchMediumId { get; set; }
    public long SessionId { get; set; }
    public int Year { get; set; }
    }
}