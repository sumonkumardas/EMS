using FluentValidation;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class EditServiceChargeRequestMessageValidator : AbstractValidator<EditServiceChargeRequestMessage>
  {
    private readonly IUniqueServiceChargePropertyChecker _effectiveDateUnique;
    public EditServiceChargeRequestMessageValidator(IUniqueServiceChargePropertyChecker effectiveDateUnique)
    {
      _effectiveDateUnique = effectiveDateUnique;
      RuleFor(x => x.PerStudentCharge).NotEmpty().WithMessage("Please specify Per Student Charge");
      RuleFor(x => x.PerStudentCharge).NotNull().WithMessage("Please specify Per Student Charge");
      RuleFor(x => x.PerStudentCharge).GreaterThanOrEqualTo(0).WithMessage("Per Student Charge can not be negative");
      RuleFor(x => x.PaymentBufferPeriod).NotEmpty().WithMessage("Please specify Payment Buffer Period");
      RuleFor(x => x.PaymentBufferPeriod).NotNull().WithMessage("Please specify Payment Buffer Period");
      RuleFor(x => x.PaymentBufferPeriod).GreaterThanOrEqualTo(0).WithMessage("Payment Buffer Period can not be negative");
      RuleFor(x => x.EffectiveDate).NotEmpty().WithMessage("Please specify Effective Date");
      RuleFor(x => x.EffectiveDate).NotNull().WithMessage("Please specify Effective Date");
      RuleFor(x => x.EndDate).GreaterThan(x => x.EffectiveDate)
        .WithMessage("End Date Must be greater than Effective Date.");
      RuleFor(x => x).Must((m, x) => IsUniqueEffectiveDate(x.EffectiveDate, m.BranchMediumId, m.ServiceChargeId)).WithMessage(
        "Same Effictive date already exists in this Branch Medium.");
    }

    private bool IsUniqueEffectiveDate(DateTime effectiveDate, long branchMediumId, long serviceChargeId)
    {
      return _effectiveDateUnique.IsUniqueEffectiveDate(effectiveDate, branchMediumId, serviceChargeId);
    }

  }
}
