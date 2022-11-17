using FluentValidation;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class AddServiceChargeRequestMessageValidator : AbstractValidator<AddServiceChargeRequestMessage>
  {
    private readonly IUniqueServiceChargePropertyChecker _effectiveDateUnique;
    public AddServiceChargeRequestMessageValidator(IUniqueServiceChargePropertyChecker effectiveDateUnique)
    {
      _effectiveDateUnique = effectiveDateUnique;
      RuleFor(x => x.PerStudentCharge).NotEmpty().WithMessage("Please specify Per Student Charge");
      RuleFor(x => x.PerStudentCharge).NotNull().WithMessage("Please specify Per Student Charge");
      RuleFor(x => x.PerStudentCharge).GreaterThanOrEqualTo(0) .WithMessage("Per Student Charge can not be negative");
      RuleFor(x => x.PaymentBufferPeriod).NotEmpty().WithMessage("Please specify Payment Buffer Period");
      RuleFor(x => x.PaymentBufferPeriod).NotNull().WithMessage("Please specify Payment Buffer Period");
      RuleFor(x => x.PaymentBufferPeriod).GreaterThanOrEqualTo(0).WithMessage("Payment Buffer Period can not be negative");
      RuleFor(x => x.EffectiveDate).NotEmpty().WithMessage("Please specify Effective Date");
      RuleFor(x => x.EffectiveDate).NotNull().WithMessage("Please specify Effective Date");
      RuleFor(x => x.EffectiveDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Effective Date Must be Today or a Future Date.");
      RuleFor(x => x).Must((m, x) => IsUniqueEffectiveDate(x.EffectiveDate, m.BranchMediumId)).WithMessage(
        "Same Effictive date already exists in this Branch Medium.");
    }

    private bool IsUniqueEffectiveDate(DateTime effectiveDate, long branchMediumId)
    {
      return _effectiveDateUnique.IsUniqueEffectiveDate(effectiveDate, branchMediumId);
    }
  }
}
