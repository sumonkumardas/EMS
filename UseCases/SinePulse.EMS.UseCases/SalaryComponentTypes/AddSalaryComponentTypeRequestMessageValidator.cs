using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class AddSalaryComponentTypeRequestMessageValidator : AbstractValidator<AddSalaryComponentTypeRequestMessage>
  {
    private readonly IUniqueSalaryComponentTypePropertyChecker _uniqueSalaryComponentTypePropertyChecker;

    public AddSalaryComponentTypeRequestMessageValidator(IUniqueSalaryComponentTypePropertyChecker uniqueSalaryComponentTypePropertyChecker)
    {
      _uniqueSalaryComponentTypePropertyChecker = uniqueSalaryComponentTypePropertyChecker;

      RuleFor(x => x.ComponentType).NotEmpty().WithMessage("Please specify Component Type");
      RuleFor(x => x.ComponentType).NotNull().WithMessage("Please specify Component Type");
      RuleFor(x => x.ComponentType).MaximumLength(20).WithMessage("Component Type is too long");
      RuleFor(x => x.ComponentType).Must(IsUniqueComponentTypeName).WithMessage("Component Type already exists.");
    }

    private bool IsUniqueComponentTypeName(string ComponentType)
    {
      return _uniqueSalaryComponentTypePropertyChecker.IsUniqueComponentType(ComponentType);
    }
  }
}
