using FluentValidation;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.AcademicFeeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.AcademicFees
{
  public class AddAcademicFeeRequestMessageValidator : AbstractValidator<AddAcademicFeeRequestMessage>
  {
    private readonly IBranchMediumAccountsHeadPropertyChecker _branchMediumAccountsHeadPropertyChecker;
    private readonly IAddAcademicFeePropertyChecker _addAcademicFeePropertyChecker;
    public AddAcademicFeeRequestMessageValidator(IBranchMediumAccountsHeadPropertyChecker branchMediumAccountsHeadPropertyChecker, IAddAcademicFeePropertyChecker addAcademicFeePropertyChecker)
    {
      _branchMediumAccountsHeadPropertyChecker = branchMediumAccountsHeadPropertyChecker;
      _addAcademicFeePropertyChecker = addAcademicFeePropertyChecker;
      RuleFor(x => x)
        .Must((m, x) => IsChartsOfAccountsImportedInCurrentSession(m.SessionId, m.BranchMediumId))
        .WithMessage("Chart of Accounts Not Imported in Session");
      RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("Please Select Class");
      RuleFor(x => x.SessionId).GreaterThan(0).WithMessage("Please Select Session");
      RuleFor(x => x).Must((m, x)=> IsFeesValid(m.BranchMediumId, m.SessionId, m.AcademicFeePeriod, m.FeesCollection))
        .WithMessage("Enter Valid Fees.");
    }

    private bool IsFeesValid(long branchMediumId, long sessionId, AcademicFeePeriodEnum academicFeePeriod,
      decimal[] feesCollection)
    {
      return _addAcademicFeePropertyChecker.IsValidFees(branchMediumId, sessionId, academicFeePeriod, feesCollection);
    }

    private bool IsChartsOfAccountsImportedInCurrentSession(long sessionId, long branchMediumId)
    {
      return _branchMediumAccountsHeadPropertyChecker.IsAlreadyCOAImportedInSession(sessionId, branchMediumId);
    }
  }
}