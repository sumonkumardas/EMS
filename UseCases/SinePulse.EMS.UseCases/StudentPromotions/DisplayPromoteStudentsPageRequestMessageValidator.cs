using FluentValidation;
using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class DisplayPromoteStudentsPageRequestMessageValidator
    : AbstractValidator<DisplayPromoteStudentsPageRequestMessage>
  {
    public DisplayPromoteStudentsPageRequestMessageValidator()
    {
    }
  }
}