using FluentValidation.Results;

namespace SinePulse.EMS.Messages.PublicHolidayMessages
{
  public class EditPublicHolidayResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditPublicHolidayResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}