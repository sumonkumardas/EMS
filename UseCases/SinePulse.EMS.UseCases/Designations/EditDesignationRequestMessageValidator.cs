using FluentValidation;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Designations
{
  public class EditDesignationRequestMessageValidator : AbstractValidator<EditDesignationRequestMessage>
  {
    private readonly IUniqueDesignationPropertyChecker _uniqueDesignationPropertyChecker;

    public EditDesignationRequestMessageValidator(IUniqueDesignationPropertyChecker uniqueDesignationPropertyChecker)
    {
      _uniqueDesignationPropertyChecker = uniqueDesignationPropertyChecker;
      RuleFor(x => x.DesignationName).NotEmpty().WithMessage("Please specify Designation Name.");
      RuleFor(x => x.DesignationName).NotNull().WithMessage("Please specify Designation Name.");
      RuleFor(x => x).Must((m, x) => IsUniqueDesignationName(m.DesignationName, m.DesignationId)).WithMessage("Designation Name already exists.");
    }

    private bool IsUniqueDesignationName(string designationName, long designationId)
    {
      return _uniqueDesignationPropertyChecker.IsUniqueDesignationName(designationName, designationId);
    }
  }
}