using FluentValidation;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Designations
{
  public class AddDesignationRequestMessageValidator : AbstractValidator<AddDesignationRequestMessage>
  {
    private readonly IUniqueDesignationPropertyChecker _uniqueDesignationPropertyChecker;

    public AddDesignationRequestMessageValidator(IUniqueDesignationPropertyChecker uniqueDesignationPropertyChecker)
    {
      _uniqueDesignationPropertyChecker = uniqueDesignationPropertyChecker;
      RuleFor(x => x.DesignationName).NotEmpty().WithMessage("Please specify Designation Name.");
      RuleFor(x => x.DesignationName).NotNull().WithMessage("Please specify Designation Name.");
      RuleFor(x => x.DesignationName).Must(IsUniqueDesignationName).WithMessage("Designation Name already exists.");
    }

    private bool IsUniqueDesignationName(string designationName)
    {
      return _uniqueDesignationPropertyChecker.IsUniqueDesignationName(designationName);
    }
  }
}