using System;
using FluentValidation;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class AddSessionRequestMessageValidator : AbstractValidator<AddSessionRequestMessage>
  {
    private readonly IUniqueSessionPropertyChecker _uniqueSessionPropertyChecker;
    private readonly ISessionOverlappingChecker _sessionOverlappingChecker;

    public AddSessionRequestMessageValidator(IUniqueSessionPropertyChecker uniqueSessionPropertyChecker,
      ISessionOverlappingChecker sessionOverlappingChecker)
    {
      _uniqueSessionPropertyChecker = uniqueSessionPropertyChecker;
      _sessionOverlappingChecker = sessionOverlappingChecker;

      RuleFor(x => x.SessionName).NotEmpty().WithMessage("Please specify Session");
      RuleFor(x => x.SessionName).MaximumLength(200).WithMessage("Session name is too long.");
      RuleFor(x => x.SessionName).NotNull().WithMessage("Please specify Session");
      RuleFor(x => x.SessionName).Must(IsUniqueSessionName).WithMessage("Session name already exists.");
      RuleFor(x => x.StartDate).NotEmpty().WithMessage("Please specify Session Start Date");
      RuleFor(x => x.StartDate).NotNull().WithMessage("Please specify Session Start Date");
      RuleFor(x => x.StartDate).Must(IsInCurrentYearOrGreater)
        .WithMessage("Entered Session Start date, neither in current year nor a future date.");
      RuleFor(x => x.EndDate).NotEmpty().WithMessage("Please specify Session End Date");
      RuleFor(x => x.EndDate).NotNull().WithMessage("Please specify Session End Date");
      RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate)
        .WithMessage("Session End Date Must be greater than Start Date.");
      RuleFor(x => x.EndDate).GreaterThan(DateTime.Today).WithMessage("Session End Date Must be a Future Date.");
      RuleFor(x => x).Must(IsNonOverlappingSessionPeriod)
        .WithMessage(x => $"Another {x.SessionType} Session exists with in this time period ");
    }

    private bool IsNonOverlappingSessionPeriod(AddSessionRequestMessage session)
    {
      switch (session.SessionType)
      {
        case ObjectTypeEnum.Global:
          return _sessionOverlappingChecker.IsNonOverlappingGlobalSessionPeriod(
            session.StartDate, session.EndDate);
        case ObjectTypeEnum.Institute:
          return _sessionOverlappingChecker.IsNonOverlappingInstituteSpecificSessionPeriod(
            session.StartDate, session.EndDate, session.ObjectId);
        case ObjectTypeEnum.Branch:
          return _sessionOverlappingChecker.IsNonOverlappingBranchSpecificSessionPeriod(
            session.StartDate, session.EndDate, session.ObjectId);
        case ObjectTypeEnum.Medium:
          return _sessionOverlappingChecker.IsNonOverlappingMediumSpecificSessionPeriod(
            session.StartDate, session.EndDate, session.ObjectId);
        case ObjectTypeEnum.BranchMedium:
          return _sessionOverlappingChecker.IsNonOverlappingBranchMediumSpecificSessionPeriod(
            session.StartDate, session.EndDate, session.ObjectId);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private bool IsInCurrentYearOrGreater(DateTime startDate)
    {
      return startDate.Year == DateTime.Now.Year || startDate.Year > DateTime.Now.Year;
    }

    private bool IsUniqueSessionName(AddSessionRequestMessage message, string sessionName)
    {
      return _uniqueSessionPropertyChecker.IsUniqueSessionName(message.ObjectId, sessionName);
    }
  }
}