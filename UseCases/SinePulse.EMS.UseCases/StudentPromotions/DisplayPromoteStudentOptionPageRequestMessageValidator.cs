using FluentValidation;
using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class DisplayPromoteStudentOptionPageRequestMessageValidator
    : AbstractValidator<DisplayPromoteStudentOptionPageRequestMessage>
  {
    public DisplayPromoteStudentOptionPageRequestMessageValidator()
    {
    }
  }
}