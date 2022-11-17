using System;
using FluentValidation;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Terms
{
  public class AddTermRequestMessageValidator : AbstractValidator<AddTermRequestMessage>
  {
    private readonly IUniqueTermPropertyChecker _uniqueTermPropertyChecker;
    private readonly ITermOverlappingChecker _termOverlappingChecker;

    public AddTermRequestMessageValidator(IUniqueTermPropertyChecker uniqueTermPropertyChecker, ITermOverlappingChecker termOverlappingChecker)
    {
      _uniqueTermPropertyChecker = uniqueTermPropertyChecker;
      _termOverlappingChecker = termOverlappingChecker;
      RuleFor(x => x.TermName).NotEmpty().WithMessage("Please specify Term Name");
      RuleFor(x => x.TermName).NotNull().WithMessage("Please specify Term Name");
      RuleFor(x => x.TermName).MaximumLength(200).WithMessage("Term Name is too long");
      RuleFor(x => x.TermName).Must((m, x) => IsUniqueTermName(x, m.SessionId,m.BranchMediumId))
        .WithMessage("Term Name already exists.");
      RuleFor(x => x.StartDate).NotEmpty().WithMessage("Please specify Start Date of the Term");
      RuleFor(x => x.StartDate).NotNull().WithMessage("Please specify Start Date of the Term");
      RuleFor(x => x.EndDate).NotEmpty().WithMessage("Please specify End Date of the Term");
      RuleFor(x => x.EndDate).NotNull().WithMessage("Please specify End Date of the Term");
      //RuleFor(x => x.StartDate).Must((m, x) => IsStartDateRangeWithinSessionPeriod(m.StartDate, m.SessionId,m.BranchMediumId))
      //  .WithMessage(x => "Start Date must be between session time period.");
      //RuleFor(x => x.EndDate).Must((m, x) => IsEndDateRangeWithinSessionPeriod(m.EndDate, m.SessionId,m.BranchMediumId))
      //  .WithMessage(x => "End Date must be between session time period.");
      //RuleFor(x => x.StartDate).Must((m, x) => IsStartDateRangeWithinTermPeriod(m.StartDate, m.SessionId, m.BranchMediumId))
      //  .WithMessage(x => "Another term overlaps with this start date.");
      //RuleFor(x => x.EndDate).Must((m, x) => IsEndDateRangeWithinTermPeriod(m.EndDate, m.SessionId, m.BranchMediumId))
      //  .WithMessage(x => "Another term overlaps with this end date.");
    }

    private bool IsUniqueTermName(string termName, long sessionId,long branchMediumId)
    {
      return _uniqueTermPropertyChecker.IsUniqueTermName(termName, sessionId,branchMediumId);
    }

    private bool IsStartDateRangeWithinSessionPeriod(DateTime startDate, long sessionId, long branchMediumId)
    {
      return _termOverlappingChecker.IsNonOverlappingWithSessionStartTime(startDate, sessionId, branchMediumId);
    }

    private bool IsEndDateRangeWithinSessionPeriod(DateTime endDate, long sessionId, long branchMediumId)
    {
      return _termOverlappingChecker.IsNonOverlappingWithSessionEndTime(endDate, sessionId, branchMediumId);
    }

    private bool IsStartDateRangeWithinTermPeriod(DateTime startDate, long sessionId, long branchMediumId)
    {
      return _termOverlappingChecker.IsNonOverlappingWithTermStartTime(startDate, sessionId, branchMediumId);
    }

    private bool IsEndDateRangeWithinTermPeriod(DateTime endDate, long sessionId, long branchMediumId)
    {
      return _termOverlappingChecker.IsNonOverlappingWithTermEndTime(endDate, sessionId, branchMediumId);
    }
  }
}