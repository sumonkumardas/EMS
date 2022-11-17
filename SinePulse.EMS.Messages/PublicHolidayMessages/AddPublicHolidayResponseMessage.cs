using FluentValidation.Results;

namespace SinePulse.EMS.Messages.PublicHolidayMessages
{
  public class AddPublicHolidayResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long PublicHolidayId { get; }

    public AddPublicHolidayResponseMessage(ValidationResult validationResult, long publicHolidayId)
    {
      ValidationResult = validationResult;
      PublicHolidayId = publicHolidayId;
    }
  }
}