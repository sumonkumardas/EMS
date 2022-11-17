using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Holidays;

namespace SinePulse.EMS.Messages.PublicHolidayMessages
{
  public class ShowPublicHolidayResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public PublicHolidayMessageModel PublicHoliday { get; set; }

    public ShowPublicHolidayResponseMessage(ValidationResult validationResult, PublicHolidayMessageModel publicHoliday)
    {
      ValidationResult = validationResult;
      PublicHoliday = publicHoliday;
    }
  }
}