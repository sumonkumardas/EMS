using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.StudentPromotionMessages
{
  public class PromoteStudentOptionRequestMessage : IRequest<ValidatedData<PromoteStudentOptionResponseMessage>>
  {
    public long NextSessionId { get; set; }
    public long ClassId { get; set; }
  }
}