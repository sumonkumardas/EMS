using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.StudentPromotionMessages
{
  public class DisplayPromoteStudentsPageRequestMessage
    : IRequest<ValidatedData<DisplayPromoteStudentsPageResponseMessage>>
  {
    public long CurrentSectionId { get; set; }
    public long NextSessionId { get; set; }
    public long NextClassId { get; set; }
  }
}