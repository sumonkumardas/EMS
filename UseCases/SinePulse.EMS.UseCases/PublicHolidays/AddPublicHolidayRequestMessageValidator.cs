using System;
using FluentValidation;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class AddPublicHolidayRequestMessageValidator : AbstractValidator<AddPublicHolidayRequestMessage>
  {
    private readonly IUniquePublicHolidayPropertyChecker _uniquePublicHolidayPropertyChecker;

    public AddPublicHolidayRequestMessageValidator(
      IUniquePublicHolidayPropertyChecker uniquePublicHolidayPropertyChecker)
    {
      _uniquePublicHolidayPropertyChecker = uniquePublicHolidayPropertyChecker;

      RuleFor(x => x.HolidayName).NotEmpty().WithMessage("Please specify Holiday Name");
      RuleFor(x => x.HolidayName).MinimumLength(10).WithMessage("Holiday Name is too small.");
      RuleFor(x => x.HolidayName).MaximumLength(200).WithMessage("Holiday Name is too long.");
      RuleFor(x => x.EndDate).Must((m, x) => IsDateRangeIsCorrect(m.StartDate, m.EndDate))
        .WithMessage("End Date must be same or later of Start Date");

      RuleFor(x => x.HolidayName).Must((m, x) => IsUniquePublicHolidayName(x, m.StartDate.Year))
        .WithMessage("Holiday Name already exists for the same year.");
    }

    private bool IsUniquePublicHolidayName(string holidayName, int year)
    {
      return _uniquePublicHolidayPropertyChecker.IsUniquePublicHolidayName(holidayName, year);
    }

    private bool IsDateRangeIsCorrect(DateTime startDate, DateTime endDate)
    {
      return endDate.Date >= startDate.Date;
    }
  }
}