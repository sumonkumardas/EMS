using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.StudentPromotionMessages
{
  public class DisplayPromoteStudentOptionPageRequestMessage
    : IRequest<ValidatedData<DisplayPromoteStudentOptionPageResponseMessage>>
  {
    public long SectionId { get; set; }
  }
}