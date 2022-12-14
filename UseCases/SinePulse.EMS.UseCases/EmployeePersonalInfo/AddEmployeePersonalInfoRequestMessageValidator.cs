using FluentValidation;
using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;

namespace SinePulse.EMS.UseCases.EmployeePersonalInfo
{
  public class AddEmployeePersonalInfoRequestMessageValidator : AbstractValidator<AddEmployeePersonalInfoRequestMessage>
  {
    public AddEmployeePersonalInfoRequestMessageValidator()
    {
      RuleFor(x => x.FatherName).NotEmpty().WithMessage("Please specify Father's name");
      RuleFor(x => x.FatherName).MaximumLength(200).WithMessage("Father's name is too long.");
      RuleFor(x => x.FatherName).NotNull().WithMessage("Please specify Father's name");

      RuleFor(x => x.MotherName).NotEmpty().WithMessage("Please specify Mother's name");
      RuleFor(x => x.MotherName).MaximumLength(200).WithMessage("Mother's name is too long.");
      RuleFor(x => x.MotherName).NotNull().WithMessage("Please specify Mother's name");

      RuleFor(x => x.Gender).NotEmpty().WithMessage("Please specify Gender type");
      RuleFor(x => x.Gender).NotNull().WithMessage("Please specify Gender Type");

      RuleFor(x => x.Religion).NotEmpty().WithMessage("Please specify Religion type");
      RuleFor(x => x.Religion).NotNull().WithMessage("Please specify Religion Type");

      RuleFor(x => x.BloodGroup).NotEmpty().WithMessage("Please specify Blood group type");
      RuleFor(x => x.BloodGroup).NotNull().WithMessage("Please specify Blood group Type");

      RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Please specify Employee");
      RuleFor(x => x.EmployeeId).NotNull().WithMessage("Please specify Employee");
    }
  }
}