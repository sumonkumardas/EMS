using FluentValidation;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.FeeCollection;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.FeeCollections
{
  public class FeeCollectionRequestMessageValidator : AbstractValidator<FeeCollectionRequestMessage>
  {
    private readonly IFeeCollectionValidationChecker _feeCollectionValidationChecker;

    public FeeCollectionRequestMessageValidator(IFeeCollectionValidationChecker feeCollectionValidationChecker)
    {
      _feeCollectionValidationChecker = feeCollectionValidationChecker;
      RuleFor(x => x.BankAccountAccountHeadId).GreaterThan(0).When(x => x.PaymentMethod == PaymentMethod.ByBank)
        .WithMessage("Select Bank Account.");
      RuleFor(x => x.SessionId).GreaterThan(0).WithMessage("Select Session.");
      RuleFor(x => x.Month).Must((m, x) => IsMonthlyFeesAlreadyAdded(m.Month, m.SessionId, m.StudentId, m.FeeType))
        .WithMessage(x => "Fees for " + x.Month.ToString("G") + " already collected.");
      RuleFor(x => x).Must((m, x) => IsYearlyFeesAlreadyAdded(m.SessionId, m.StudentId, m.FeeType))
        .WithMessage("Yearly Fees already collected.");
    }

    private bool IsYearlyFeesAlreadyAdded(long sessionId, long studentId, AcademicFeePeriodEnum feeType)
    {
      return _feeCollectionValidationChecker.IsYearlyFeesAlreadyCollected(sessionId, studentId, feeType);
    }

    private bool IsMonthlyFeesAlreadyAdded(MonthType month, long sessionId, long studentId,
      AcademicFeePeriodEnum feeType)
    {
      return _feeCollectionValidationChecker.IsMonthlyFeesAlreadyCollected(month, sessionId, studentId, feeType);
    }
  }
}