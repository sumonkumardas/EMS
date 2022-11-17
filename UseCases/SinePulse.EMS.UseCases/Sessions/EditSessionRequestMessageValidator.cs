using System;
using FluentValidation;
using FluentValidation.Validators;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class EditSessionRequestMessageValidator : AbstractValidator<EditSessionRequestMessage>
  {
    private readonly IUniqueSessionPropertyChecker _uniqueSessionPropertyChecker;

    public EditSessionRequestMessageValidator(IUniqueSessionPropertyChecker uniqueSessionPropertyChecker)
    {
      _uniqueSessionPropertyChecker = uniqueSessionPropertyChecker;

      RuleFor(x => x.SessionName).NotEmpty().WithMessage("Please specify Session");
      RuleFor(x => x.SessionName).MaximumLength(200).WithMessage("Session name is too long.");
      RuleFor(x => x.SessionName).NotNull().WithMessage("Please specify Session");
      RuleFor(x => x.SessionName).Must(IsUniqueSessionName).WithMessage("Session Name already exists.");
      RuleFor(x => x.StartDate).NotEmpty().WithMessage("Please specify Session Start Date");
      RuleFor(x => x.StartDate).NotNull().WithMessage("Please specify Session Start Date");
      RuleFor(x => x.StartDate).Must(IsInCurrentYearOrGreater)
        .WithMessage("Entered Session Start date, neither in current year nor a future date.");
      RuleFor(x => x.EndDate).NotEmpty().WithMessage("Please specify Session End Date");
      RuleFor(x => x.EndDate).NotNull().WithMessage("Please specify Session End Date");
      RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate)
        .WithMessage("Session End Date Must be greater than Start Date.");
      RuleFor(x => x.EndDate).GreaterThan(DateTime.Today).WithMessage("Session End Date Must be a Future Date.");
      RuleFor(x => x).Custom(IsSessionExists);
      RuleFor(x => x.Status).NotNull().WithMessage("Select Status");
      RuleFor(x => x.Status).IsInEnum().WithMessage("Select Status");
    }

    private void IsSessionExists(EditSessionRequestMessage session, CustomContext context)
    {
      if (_uniqueSessionPropertyChecker.IsSessionExists(session.StartDate, session.EndDate, session.SessionId))
        context.AddFailure("A Session with same Start & End date already exists.");
    }

    private bool IsInCurrentYearOrGreater(DateTime startDate)
    {
      return startDate.Year == DateTime.Now.Year || startDate.Year > DateTime.Now.Year;
    }

    private bool IsUniqueSessionName(EditSessionRequestMessage session, string sessionName)
    {
      return _uniqueSessionPropertyChecker.IsUniqueSessionName(session.SessionName, session.SessionId);
    }
  }
}