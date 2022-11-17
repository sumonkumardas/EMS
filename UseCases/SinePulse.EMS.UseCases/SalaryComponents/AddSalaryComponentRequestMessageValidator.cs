using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class AddSalaryComponentRequestMessageValidator : AbstractValidator<AddSalaryComponentRequestMessage>
  {
    private readonly IUniqueSalaryComponentPropertyChecker _uniqueSalaryComponentPropertyChecker;

    public AddSalaryComponentRequestMessageValidator(IUniqueSalaryComponentPropertyChecker uniqueSalaryComponentPropertyChecker)
    {
      _uniqueSalaryComponentPropertyChecker = uniqueSalaryComponentPropertyChecker;

      RuleFor(x => x.ComponentName).NotEmpty().WithMessage("Please specify Component Type");
      RuleFor(x => x.ComponentName).NotNull().WithMessage("Please specify Component Type");
      RuleFor(x => x.ComponentName).MaximumLength(20).WithMessage("Component Type is too long");
      RuleFor(x => x.ComponentName).Must(IsUniqueComponentName).WithMessage("Component Type already exists.");
    }

    private bool IsUniqueComponentName(string ComponentName)
    {
      return _uniqueSalaryComponentPropertyChecker.IsUniqueComponentName(ComponentName);
    }
  }
}
