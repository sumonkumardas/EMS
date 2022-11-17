using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.UseCases.Repositories;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class AddBranchMediumRequestMessageValidator : AbstractValidator<AddBranchMediumRequestMessage>
  {
    private readonly IMediumNotPreviouslyAddedChecker _mediumNotPreviouslyAdded;
    private readonly IMediumExistanceChecker _mediumExistanceChecker;
    private readonly IShiftExistanceChecker _shiftExistanceChecker;
    public AddBranchMediumRequestMessageValidator(IMediumNotPreviouslyAddedChecker mediumNotPreviouslyAdded, 
      IMediumExistanceChecker mediumExistanceChecker, IShiftExistanceChecker shiftExistanceChecker)
    {
      _mediumNotPreviouslyAdded = mediumNotPreviouslyAdded;
      _mediumExistanceChecker = mediumExistanceChecker;
      _shiftExistanceChecker = shiftExistanceChecker;
      RuleFor(_ => _.WeeklyHolidaysList).Must((m, x) => IsValidWeeklyHolidays(m.WeeklyHolidaysList))
        .WithMessage("At least one or maximum two weekly holiday should be configured.");
      RuleFor(x => x).Must((m, x) => IsMediumNotPreviouslyAdded(x.MediumId, x.BranchId)).WithMessage(
        "This Medium already added.");
      RuleFor(x => x.MediumId).Must(IsMediumExists).WithMessage("Select Medium");
      RuleFor(x => x.ShiftId).Must(IsShiftExists).WithMessage("Select Shift");
      RuleFor(x => x.SessionBufferPeriod).GreaterThanOrEqualTo(0).WithMessage(
        "Please specify valid session buffer period.");
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

    private bool IsShiftExists(long shiftId)
    {
      return _shiftExistanceChecker.IsShiftExists(shiftId);
    }

    private bool IsMediumExists(long mediumId)
    {
      return _mediumExistanceChecker.IsMediumExists(mediumId);
    }

    private bool IsMediumNotPreviouslyAdded(long mediumId, long branchId)
    {
      return _mediumNotPreviouslyAdded.IsMediumPreviouslyAdded(mediumId, branchId);
    }
  }
}