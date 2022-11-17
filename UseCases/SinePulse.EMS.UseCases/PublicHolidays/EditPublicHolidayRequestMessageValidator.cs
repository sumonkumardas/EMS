using System;
using FluentValidation;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class EditPublicHolidayRequestMessageValidator : AbstractValidator<EditPublicHolidayRequestMessage>
  {
    private readonly IUniquePublicHolidayPropertyChecker _uniquePublicHolidayPropertyChecker;

    public EditPublicHolidayRequestMessageValidator(
      IUniquePublicHolidayPropertyChecker uniquePublicHolidayPropertyChecker)
    {
      _uniquePublicHolidayPropertyChecker = uniquePublicHolidayPropertyChecker;

      RuleFor(x => x.HolidayName).NotEmpty().WithMessage("Please specify Holiday Name");
      RuleFor(x => x.HolidayName).MinimumLength(10).WithMessage("Holiday Name is too small.");
      RuleFor(x => x.HolidayName).MaximumLength(200).WithMessage("Holiday Name is too long.");
      RuleFor(x => x.EndDate).Must((m, x) => IsDateRangeIsCorrect(m.StartDate, m.EndDate))
        .WithMessage("End Date must be same or later of Start Date");

      RuleFor(x => x.HolidayName).Must((m, x) => IsUniquePublicHolidayName(x, m.StartDate.Year,m.Id))
        .WithMessage("Holiday Name already exists for the same year.");
    }

    private bool IsUniquePublicHolidayName(string holidayName, int year, long publicHolidayId)
    {
      return _uniquePublicHolidayPropertyChecker.IsUniquePublicHolidayName(holidayName, year, publicHolidayId);
    }

    private bool IsDateRangeIsCorrect(DateTime startDate, DateTime endDate)
    {
      return endDate.Date >= startDate.Date;
    }
  }
}