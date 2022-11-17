using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ImportSessionDataMessages
{
  public class DisplayImportSessionDataPageRequestMessage
    : IRequest<ValidatedData<DisplayImportSessionDataPageResponseMessage>>
  {
    public long BranchMediumId { get; set; }
  }
}