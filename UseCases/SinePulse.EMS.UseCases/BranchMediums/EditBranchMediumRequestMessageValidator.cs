using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class EditBranchMediumRequestMessageValidator : AbstractValidator<EditBranchMediumRequestMessage>
  {
    private readonly IMediumNotPreviouslyAddedChecker _mediumNotPreviouslyAdded;
    public EditBranchMediumRequestMessageValidator(IMediumNotPreviouslyAddedChecker mediumNotPreviouslyAdded)
    {
      _mediumNotPreviouslyAdded = mediumNotPreviouslyAdded;
      RuleFor(_ => _.WeeklyHolidaysList).Must((m, x) => IsValidWeeklyHolidays(m.WeeklyHolidaysList))
        .WithMessage("At least one or maximum two weekly holiday should be configured.");
      RuleFor(_ => _.MediumId).Must(IsMediumNotPreviouslyAdded).WithMessage(
        "This Medium already added.");
      RuleFor(x => x.SessionBufferPeriod).GreaterThanOrEqualTo(0).WithMessage(
        "Please specify valid session buffer period.");
    }

    private bool IsMediumNotPreviouslyAdded(EditBranchMediumRequestMessage message,long mediumId)
    {
      return _mediumNotPreviouslyAdded.IsMediumPreviouslyAdded(mediumId, message.BranchId, message.BranchMediumId);
    }
    
    private bool IsValidWeeklyHolidays(IEnumerable<WeekDays> weeklyHolidaysList)
    {
      if (weeklyHolidaysList != null)
      {
        var numOfHolidays = weeklyHolidaysList.Count();
        return numOfHolidays > 0 && numOfHolidays < 3;
      }
      return false;
    }
  }
}