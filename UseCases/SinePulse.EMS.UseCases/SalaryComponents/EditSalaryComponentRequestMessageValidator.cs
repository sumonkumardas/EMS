using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class EditSalaryComponentRequestMessageValidator : AbstractValidator<EditSalaryComponentRequestMessage>
  {
    private readonly IUniqueSalaryComponentPropertyChecker _uniqueSalaryComponentPropertyChecker;

    public EditSalaryComponentRequestMessageValidator(IUniqueSalaryComponentPropertyChecker uniqueSalaryComponentPropertyChecker)
    {
      _uniqueSalaryComponentPropertyChecker = uniqueSalaryComponentPropertyChecker;

      RuleFor(x => x.ComponentName).NotEmpty().WithMessage("Please specify Component Type");
      RuleFor(x => x.ComponentName).NotNull().WithMessage("Please specify Component Type");
      RuleFor(x => x.ComponentName).MaximumLength(20).WithMessage("Component Type is too long");
      RuleFor(x => x.ComponentName).Must(IsUniqueComponentName).WithMessage("Component Type already exists.");
    }

    private bool IsUniqueComponentName(EditSalaryComponentRequestMessage requestMessage,string ComponentName)
    {
      return _uniqueSalaryComponentPropertyChecker.IsUniqueComponentName(ComponentName, requestMessage.ComponentId);
    }
  }
}
