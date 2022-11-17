using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class EditSalaryComponentTypeRequestMessageValidator : AbstractValidator<EditSalaryComponentTypeRequestMessage>
  {
    private readonly IUniqueSalaryComponentTypePropertyChecker _uniqueSalaryComponentTypePropertyChecker;

    public EditSalaryComponentTypeRequestMessageValidator(IUniqueSalaryComponentTypePropertyChecker uniqueSalaryComponentTypePropertyChecker)
    {
      _uniqueSalaryComponentTypePropertyChecker = uniqueSalaryComponentTypePropertyChecker;

      RuleFor(x => x.ComponentType).NotEmpty().WithMessage("Please specify Component Type");
      RuleFor(x => x.ComponentType).NotNull().WithMessage("Please specify Component Type");
      RuleFor(x => x.ComponentType).MaximumLength(20).WithMessage("Component Type is too long");
      RuleFor(x => x.ComponentType).Must(IsUniqueComponentTypeName).WithMessage("Component Type already exists.");
    }

    private bool IsUniqueComponentTypeName(EditSalaryComponentTypeRequestMessage message,string ComponentType)
    {
      return _uniqueSalaryComponentTypePropertyChecker.IsUniqueComponentType(ComponentType, message.SalaryComponentTypeId);
    } 
  }
}
